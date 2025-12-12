namespace SvSWebApiTips.Models
{
    public record MatchInfoDto(
        int MatchNo,
        string HomeTeam,
        string AwayTeam,
        decimal Odds1,
        decimal OddsX,
        decimal Odds2,
        decimal FolkPct1,
        decimal FolkPctX,
        decimal FolkPct2,
        decimal FolkOdds1,
        decimal FolkOddsX,
        decimal FolkOdds2,
        decimal OddsPct1,
        decimal OddsPctX,
        decimal OddsPct2,
        decimal Kvot1,
        decimal KvotX,
        decimal Kvot2
    );

}
