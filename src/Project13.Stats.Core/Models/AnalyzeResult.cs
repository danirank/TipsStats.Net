using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public sealed record AnalyzeResult
    (
        //Antal Omg
        int SampleSize,
        int PeopleWasWrong, 
        bool BiggesFavNotWin,
        bool BiggesFavWin,
        // Favoritfall
        decimal MedianTotDiff,
        decimal P25TotDiff,
        decimal P75TotDiff 


    );
}
