using System.Collections.Generic;

namespace Project13.Stats.Core.Models
{
    public class CalculationModel
    {
        // Identitet
        public int Id { get; set; }
        public string Tips { get; set; } = string.Empty;
        public int? Vecka { get; set; }

        // Summering-data
        public decimal? Utdelning13 { get; set; }
        public int? Antal13 { get; set; }
        public decimal? Omsattning { get; set; }

        // Matcher (DTO-version)
        public List<MatchDto> Matcher { get; set; } = new();

        // Oddsprofiler
        public decimal MultipliceradeOddsMarknad { get; set; }
        public decimal MultipliceradeOddsSvFolket { get; set; }
        public decimal OddsKvotTotal { get; set; }              // Nytt namn, tydligare
        public decimal OddsKvotPerMatch { get; set; }           // Nytt
        public decimal OddsFavoritrad { get; set; }
        public decimal OddsFavoritradSvF { get; set; }
        public decimal KvotFavoritRad { get; set; }

        // Favorit/scräll-profil
        public int AntalStorfavoriter { get; set; }
        public int AntalMellanFavoriter { get; set; }
        public int AntalStorfavoriterVinst { get; set; }
        public int AntalMellanFavoriterVinst { get; set; }
        public int TotFavoriter { get; set; }
        public int TotVinstFav { get; set; }
        public int TotDiff { get; set; }

        public decimal SkrallIndexProcent { get; set; }
        public int AntalSkrällar { get; set; }                       // Nytt
        public decimal FavoritIndexProcent { get; set; }
        public decimal StorFavoritTraffProcent { get; set; }
        public decimal MellanFavoritTraffProcent { get; set; }

        // Folket vs Odds – edge
        public decimal FolketsFelProcent { get; set; }
        public decimal OddsetFelProcent { get; set; }

        // Disagreement-index
        public decimal MeanDisagreement { get; set; }                // Nytt
        public decimal MaxDisagreement { get; set; }                 // Nytt
        public decimal DisagreementTop3Sum { get; set; }             // Nytt
    }
}
