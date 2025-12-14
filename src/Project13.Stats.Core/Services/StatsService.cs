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

            query = StatsFilter.ApplyFilter(query, filter);

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


    }
}
