using Microsoft.EntityFrameworkCore;
using Project13.Stats.Core.Data;
using Project13.Stats.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Services
{
    public class StatsService
    {
        private readonly AppDbContext _db;

        public StatsService(AppDbContext db)
        {
            _db = db;
        }


        public async Task<List<CalculationDto>> GetAllAsync()
        {
            var result = await _db.Calculations
                    .Select(c => new CalculationDto(
                        c.Id,
                        c.SvSpelInfoId,
                        c.Vecka,
                        c.Tips,

                        c.Utdelning13,
                        c.Antal13,
                        c.Omsattning,

                        c.MultipliceradeOddsMarknad,
                        c.MultipliceradeOddsSvFolket,

                        c.OddsKvotTotal,
                        c.OddsKvotPerMatch,

                        c.OddsFavoritrad,
                        c.OddsFavoritradSvF,
                        c.KvotFavoritRad,

                        c.AntalStorfavoriter,
                        c.AntalMellanFavoriter,
                        c.AntalStorfavoriterVinst,
                        c.AntalMellanFavoriterVinst,

                        c.TotFavoriter,
                        c.TotVinstFav,
                        c.TotDiff,

                        c.StrongestFavoriteWon,
                        c.StrongestFavoriteOdds,
                        c.StrongestFavoriteDisagreement,
                        c.StrongestFavoriteMatchNumber,

                        c.AntalSkrällar,
                        c.SkrallIndexProcent,
                        c.FavoritIndexProcent,

                        c.StorFavoritTraffProcent,
                        c.MellanFavoritTraffProcent,

                        c.FolketsFelProcent,
                        c.OddsetFelProcent,

                        c.MeanDisagreement,
                        c.MaxDisagreement,
                        c.DisagreementTop3Sum,

                        c.Matcher.Select(m => new MatchDto(
                            m.Matchnummer,
                            m.Hemmalag,
                            m.Bortalag,
                            m.Utfall,
                            m.Oddset1,
                            m.OddsetX,
                            m.Oddset2,
                            m.SvenskaFolket1,
                            m.SvenskaFolketX,
                            m.SvenskaFolket2,
                            m.KvotKorrektTecken,
                            m.Disagreement
                        )).ToList()
                    ))
                    .ToListAsync();

            return result;
        }




        public async Task<List<CalculationDto>> SearchAsync(StatsFilter filter)
        {
            var query = _db.Calculations.AsQueryable();

            query = ApplyFilter(query, filter);

            var result = await query
                .OrderByDescending(x => x.Vecka) // rimlig default
                .Select(c => new CalculationDto(
                    c.Id,
                    c.SvSpelInfoId,
                    c.Vecka,
                    c.Tips,

                    c.Utdelning13,
                    c.Antal13,
                    c.Omsattning,

                    c.MultipliceradeOddsMarknad,
                    c.MultipliceradeOddsSvFolket,

                    c.OddsKvotTotal,
                    c.OddsKvotPerMatch,

                    c.OddsFavoritrad,
                    c.OddsFavoritradSvF,
                    c.KvotFavoritRad,

                    c.AntalStorfavoriter,
                    c.AntalMellanFavoriter,
                    c.AntalStorfavoriterVinst,
                    c.AntalMellanFavoriterVinst,

                    c.TotFavoriter,
                    c.TotVinstFav,
                    c.TotDiff,

                    c.StrongestFavoriteWon,
                    c.StrongestFavoriteOdds,
                    c.StrongestFavoriteDisagreement,
                     c.StrongestFavoriteMatchNumber,

                     c.AntalSkrällar,
                    c.SkrallIndexProcent,
                    c.FavoritIndexProcent,

                    c.StorFavoritTraffProcent,
                    c.MellanFavoritTraffProcent,

                    c.FolketsFelProcent,
                    c.OddsetFelProcent,

                    c.MeanDisagreement,
                    c.MaxDisagreement,
                    c.DisagreementTop3Sum,

                    // matcher kan tas bort här om du vill ha "profil-only"
                    c.Matcher.Select(m => new MatchDto(
                        m.Matchnummer,
                        m.Hemmalag,
                        m.Bortalag,
                        m.Utfall,
                        m.Oddset1,
                        m.OddsetX,
                        m.Oddset2,
                        m.SvenskaFolket1,
                        m.SvenskaFolketX,
                        m.SvenskaFolket2,
                        m.KvotKorrektTecken,
                        m.Disagreement
                    )).ToList()
                ))
                .ToListAsync();

            return result;
        }


        public async Task<SearchAnalysisResult> AnalyzeAsync(StatsFilter filter, int maxSample = 500)
        {
            var q = _db.Calculations.AsNoTracking().AsQueryable();
            q = ApplyFilter(q, filter);

            var data = await q
                .Take(maxSample)   // skydd så du inte hämtar allt
                .ToListAsync();

            return BuildAnalysis(data);
        }

        private static SearchAnalysisResult BuildAnalysis(List<CalculationModel> data)
        {
            if (data == null || data.Count == 0)
            {
                return new SearchAnalysisResult(
                    SampleSize: 0,

                    TotDiff_P25: 0m,
                    TotDiff_Median: 0m,
                    TotDiff_P75: 0m,
                    P_TotDiff_AtLeast3: 0m,
                    P_TotDiff_AtLeast4: 0m,

                    Skrallar_Median: 0m,
                    P_Skrallar_AtLeast2: 0m,
                    P_Skrallar_AtLeast3: 0m,

                    Utd13_P25: 0m,
                    Utd13_Median: 0m,
                    Utd13_P75: 0m,
                    Utd13_Max: 0m,

                    MeanDisagreement_Median: 0m,
                    DisagreementTop3Sum_Median: 0m,

                    P_StrongestFavLoses: null,
                    Utd13_Median_WhenStrongestFavLoses: null,
                    TotDiff_Median_WhenStrongestFavLoses: null
                );
            }

            int n = data.Count;

            // ---------- Favoritfall ----------
            var diffs = data.Select(x => x.TotDiff).OrderBy(x => x).ToList();
            decimal pDiff3 = (decimal)data.Count(x => x.TotDiff >= 3) / n;
            decimal pDiff4 = (decimal)data.Count(x => x.TotDiff >= 4) / n;

            // ---------- Skrällar ----------
            var skr = data.Select(x => x.AntalSkrällar).OrderBy(x => x).ToList();
            decimal pSkr2 = (decimal)data.Count(x => x.AntalSkrällar >= 2) / n;
            decimal pSkr3 = (decimal)data.Count(x => x.AntalSkrällar >= 3) / n;

            // ---------- Utdelning ----------
            var utd = data
                .Select(x => x.Utdelning13 ?? 0m)
                .Where(x => x > 0m)
                .OrderBy(x => x)
                .ToList();

            // ---------- Disagreement ----------
            var meanDis = data.Select(x => x.MeanDisagreement).OrderBy(x => x).ToList();
            var top3Sum = data.Select(x => x.DisagreementTop3Sum).OrderBy(x => x).ToList();

            // ---------- Strongest favorite ----------
            var lose = data.Where(x => !x.StrongestFavoriteWon).ToList();

            decimal? pStrongLose = lose.Count > 0 ? (decimal)lose.Count / n : 0m;

            var utdLose = lose
                .Select(x => x.Utdelning13 ?? 0m)
                .Where(x => x > 0m)
                .OrderBy(x => x)
                .ToList();

            decimal? utdMedianWhenLose = utdLose.Count > 0 ? Median(utdLose) : 0m;

            var diffLose = lose.Select(x => x.TotDiff).OrderBy(x => x).ToList();
            decimal? diffMedianWhenLose = diffLose.Count > 0 ? Median(diffLose) : 0m;

            return new SearchAnalysisResult(
                SampleSize: n,

                // Favoritfall
                TotDiff_P25: Percentile(diffs, 0.25m),
                TotDiff_Median: Median(diffs),
                TotDiff_P75: Percentile(diffs, 0.75m),
                P_TotDiff_AtLeast3: pDiff3,
                P_TotDiff_AtLeast4: pDiff4,

                // Skrällar
                Skrallar_Median: Median(skr),
                P_Skrallar_AtLeast2: pSkr2,
                P_Skrallar_AtLeast3: pSkr3,

                // Utdelning
                Utd13_P25: utd.Count > 0 ? Percentile(utd, 0.25m) : 0m,
                Utd13_Median: utd.Count > 0 ? Median(utd) : 0m,
                Utd13_P75: utd.Count > 0 ? Percentile(utd, 0.75m) : 0m,
                Utd13_Max: utd.Count > 0 ? utd[^1] : 0m,

                // Disagreement / value
                MeanDisagreement_Median: Median(meanDis),
                DisagreementTop3Sum_Median: Median(top3Sum),

                // Strongest favorite
                P_StrongestFavLoses: pStrongLose,
                Utd13_Median_WhenStrongestFavLoses: utdMedianWhenLose,
                TotDiff_Median_WhenStrongestFavLoses: diffMedianWhenLose
            );
        }


        private static decimal Median(IReadOnlyList<int> sorted)
        {
            if (sorted.Count == 0) return 0m;
            int mid = sorted.Count / 2;
            return sorted.Count % 2 == 1
                ? sorted[mid]
                : (sorted[mid - 1] + sorted[mid]) / 2m;
        }

        private static decimal Median(IReadOnlyList<decimal> sorted)
        {
            if (sorted.Count == 0) return 0m;
            int mid = sorted.Count / 2;
            return sorted.Count % 2 == 1
                ? sorted[mid]
                : (sorted[mid - 1] + sorted[mid]) / 2m;
        }

        private static decimal Percentile(IReadOnlyList<int> sorted, decimal p)
        {
            if (sorted.Count == 0) return 0m;
            int idx = (int)Math.Round((double)(p * (sorted.Count - 1)));
            idx = Math.Clamp(idx, 0, sorted.Count - 1);
            return sorted[idx];
        }

        private static decimal Percentile(IReadOnlyList<decimal> sorted, decimal p)
        {
            if (sorted.Count == 0) return 0m;
            int idx = (int)Math.Round((double)(p * (sorted.Count - 1)));
            idx = Math.Clamp(idx, 0, sorted.Count - 1);
            return sorted[idx];
        }


        // Du har redan ApplyFilter(...) sedan tidigare
        public static IQueryable<CalculationModel> ApplyFilter(
                      IQueryable<CalculationModel> query,
                         StatsFilter f)
        {
            // Scope / identitet
            if (!string.IsNullOrWhiteSpace(f.Tips))
                query = query.Where(x => x.Tips == f.Tips);

            if (f.Vecka.HasValue)
                query = query.Where(x => x.Vecka == f.Vecka);

            // Svårighetsgrad – favoritrad
            if (f.MinOddsFavoritrad.HasValue)
                query = query.Where(x => x.OddsFavoritrad >= f.MinOddsFavoritrad);

            if (f.MaxOddsFavoritrad.HasValue)
                query = query.Where(x => x.OddsFavoritrad <= f.MaxOddsFavoritrad);

            // Favoriter
            if (f.MinTotFavoriter.HasValue)
                query = query.Where(x => x.TotFavoriter >= f.MinTotFavoriter);

            if (f.MaxTotFavoriter.HasValue)
                query = query.Where(x => x.TotFavoriter <= f.MaxTotFavoriter);

            if (f.MinStorFav.HasValue)
                query = query.Where(x => x.AntalStorfavoriter >= f.MinStorFav);

            if (f.MaxStorFav.HasValue)
                query = query.Where(x => x.AntalStorfavoriter <= f.MaxStorFav);

            if (f.MinMedelFav.HasValue)
                query = query.Where(x => x.AntalMellanFavoriter >= f.MinMedelFav);

            if (f.MaxMedelFav.HasValue)
                query = query.Where(x => x.AntalMellanFavoriter <= f.MaxMedelFav);

            // Skrällprofil
            if (f.MinAntalSkrallar.HasValue)
                query = query.Where(x => x.AntalSkrällar >= f.MinAntalSkrallar);

            if (f.MaxAntalSkrallar.HasValue)
                query = query.Where(x => x.AntalSkrällar <= f.MaxAntalSkrallar);

            if (f.MinSkrallIndexProcent.HasValue)
                query = query.Where(x => x.SkrallIndexProcent >= f.MinSkrallIndexProcent);

            if (f.MaxSkrallIndexProcent.HasValue)
                query = query.Where(x => x.SkrallIndexProcent <= f.MaxSkrallIndexProcent);

            // Disagreement / value
            if (f.MinMeanDisagreement.HasValue)
                query = query.Where(x => x.MeanDisagreement >= f.MinMeanDisagreement);

            if (f.MaxMeanDisagreement.HasValue)
                query = query.Where(x => x.MeanDisagreement <= f.MaxMeanDisagreement);

            if (f.MinMaxDisagreement.HasValue)
                query = query.Where(x => x.MaxDisagreement >= f.MinMaxDisagreement);

            if (f.MaxMaxDisagreement.HasValue)
                query = query.Where(x => x.MaxDisagreement <= f.MaxMaxDisagreement);

            if (f.MinDisagreementTop3Sum.HasValue)
                query = query.Where(x => x.DisagreementTop3Sum >= f.MinDisagreementTop3Sum);

            if (f.MaxDisagreementTop3Sum.HasValue)
                query = query.Where(x => x.DisagreementTop3Sum <= f.MaxDisagreementTop3Sum);

            if (f.MinOddsKvotTotal.HasValue)
                query = query.Where(x => x.OddsKvotTotal >= f.MinOddsKvotTotal);

            if (f.MaxOddsKvotTotal.HasValue)
                query = query.Where(x => x.OddsKvotTotal <= f.MaxOddsKvotTotal);

            if (f.MinKvotFavoritRad.HasValue)
                query = query.Where(x => x.KvotFavoritRad >= f.MinKvotFavoritRad);

            if (f.MaxKvotFavoritRad.HasValue)
                query = query.Where(x => x.KvotFavoritRad <= f.MaxKvotFavoritRad);

            if (f.StrongestFavoriteWon.HasValue)
                query = query.Where(x => x.StrongestFavoriteWon == f.StrongestFavoriteWon);

            if (f.MinStrongestFavoriteOdds.HasValue)
                query = query.Where(x => x.StrongestFavoriteOdds >= f.MinStrongestFavoriteOdds);

            if (f.MaxStrongestFavoriteOdds.HasValue)
                query = query.Where(x => x.StrongestFavoriteOdds <= f.MaxStrongestFavoriteOdds);


            // Outcome / analys (använd efter filtrering)
            if (f.MinUtdelning13.HasValue)
                query = query.Where(x => x.Utdelning13 >= f.MinUtdelning13);

            if (f.MaxUtdelning13.HasValue)
                query = query.Where(x => x.Utdelning13 <= f.MaxUtdelning13);

            if (f.MinAntal13.HasValue)
                query = query.Where(x => x.Antal13 >= f.MinAntal13);

            if (f.MaxAntal13.HasValue)
                query = query.Where(x => x.Antal13 <= f.MaxAntal13);

            if (f.MinTurnover.HasValue)
                query = query.Where(x => x.Omsattning >= f.MinTurnover);

            if (f.MaxTurnover.HasValue)
                query = query.Where(x => x.Omsattning <= f.MaxTurnover);

            return query;
        }



    }
}
