using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public sealed record MatchDto
(
    int Matchnummer,
    string Hemmalag,
    string Bortalag,
    string Utfall,

    decimal? Oddset1,
    decimal? OddsetX,
    decimal? Oddset2,

    decimal? SvenskaFolket1,
    decimal? SvenskaFolketX,
    decimal? SvenskaFolket2,

    decimal? KvotKorrektTecken,
    decimal Disagreement
);

}
