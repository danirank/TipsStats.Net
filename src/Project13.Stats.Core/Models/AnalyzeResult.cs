using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public sealed record SearchAnalysisResult(
   
    int SampleSize,

    // Favoritfall
    decimal TotDiff_P25,
    decimal TotDiff_Median,
    decimal TotDiff_P75,
    decimal P_TotDiff_AtLeast3,
    decimal P_TotDiff_AtLeast4,

    // Skrällar
    decimal Skrallar_Median, //Definerad som odds över 4
    decimal P_Skrallar_AtLeast2,
    decimal P_Skrallar_AtLeast3,

    // Utdelning
    decimal Utd13_P25,
    decimal Utd13_Median,
    decimal Utd13_P75,
    decimal Utd13_Max,

    // Disagreement / value
    decimal MeanDisagreement_Median,
    decimal DisagreementTop3Sum_Median,

    // Strongest favorite (om du har fälten)
    decimal? P_StrongestFavLoses,
    decimal? Utd13_Median_WhenStrongestFavLoses,
    decimal? TotDiff_Median_WhenStrongestFavLoses
);
    

}
