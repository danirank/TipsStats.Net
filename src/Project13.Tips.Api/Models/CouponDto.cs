using Project13.Tips.Api.Constants;

namespace Project13.Tips.Api.Models
{
    public sealed record CouponDto(
      string? TipType,
      int? Week,
      string? Turnover,
      IReadOnlyList<MatchInfoDto> Matches
    );


}
