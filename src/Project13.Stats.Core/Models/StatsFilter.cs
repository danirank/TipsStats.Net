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

        
    }

}


