using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public sealed class StatsFilter
    {
        // Identitet / scope
        public int? Vecka { get; set; }
        public string? Tips { get; set; }

        // Svårighet (förhand)
        public decimal? MinOddsFavoritrad { get; set; }
        public decimal? MaxOddsFavoritrad { get; set; }

        public int? MinTotFavoriter { get; set; }
        public int? MaxTotFavoriter { get; set; }

        public decimal? MinFavoritIndexProcent { get; set; }
        public decimal? MaxFavoritIndexProcent { get; set; }

        public int? MinStorFav { get; set; }
        public int? MaxStorFav { get; set; }

        public bool? StrongestFavoriteWon { get; set; }

        public decimal? MinStrongestFavoriteOdds { get; set; }
        public decimal? MaxStrongestFavoriteOdds { get; set; }


        public int? MinMedelFav { get; set; }
        public int? MaxMedelFav { get; set; }

        // Varians / skrällprofil (förhand)
        public int? MinAntalSkrallar { get; set; }
        public int? MaxAntalSkrallar { get; set; }

        public decimal? MinSkrallIndexProcent { get; set; }
        public decimal? MaxSkrallIndexProcent { get; set; }

        // Value / disagreement (förhand)
        public decimal? MinMeanDisagreement { get; set; }
        public decimal? MaxMeanDisagreement { get; set; }

        public decimal? MinMaxDisagreement { get; set; }
        public decimal? MaxMaxDisagreement { get; set; }

        public decimal? MinDisagreementTop3Sum { get; set; }
        public decimal? MaxDisagreementTop3Sum { get; set; }

        public decimal? MinOddsKvotTotal { get; set; }
        public decimal? MaxOddsKvotTotal { get; set; }

        public decimal? MinKvotFavoritRad { get; set; }
        public decimal? MaxKvotFavoritRad { get; set; }

        // Resultat/outcome (använd för analys efter filtrering)
        public decimal? MinUtdelning13 { get; set; }
        public decimal? MaxUtdelning13 { get; set; }

        public int? MinAntal13 { get; set; }
        public int? MaxAntal13 { get; set; }

        public decimal? MinTurnover { get; set; }
        public decimal? MaxTurnover { get; set; }

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


