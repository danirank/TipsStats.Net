using Project13.Tips.Api.Models;

namespace Project13.Tips.Api.Mappings
{
    public static class MatchInfoDtoMapper
    {
        public static MatchInfoDto ToDto(this MatchInfo m) => new(
        MatchNo: m.Matchnummer,
        HomeTeam: m.Hemmalag,
        AwayTeam: m.Bortalag,
        Odds1: ToDec(m.Odds1),
        OddsX: ToDec(m.OddsX),
        Odds2: ToDec(m.Odds2),
        FolkPct1: ToDec(m.SvenskaFolketProcent1),
        FolkPctX: ToDec(m.SvenskaFolketProcentX),
        FolkPct2: ToDec(m.SvenskaFolketProcent2),
        FolkOdds1: ToDec(m.SvenskaFolketOdds1),
        FolkOddsX: ToDec(m.SvenskaFolketOddsX),
        FolkOdds2: ToDec(m.SvenskaFolketOdds2),
        OddsPct1: ToDec(m.OddsProcent1),
        OddsPctX: ToDec(m.OddsProcentX),
        OddsPct2: ToDec(m.OddsProcent2),
        Kvot1: ToDec(m.Kvot1),
        KvotX: ToDec(m.KvotX),
        Kvot2: ToDec(m.Kvot2)
    );

        private static decimal ToDec(double v) => Convert.ToDecimal(v);
    }
}
