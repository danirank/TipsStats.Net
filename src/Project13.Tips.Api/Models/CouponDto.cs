using Project13.Tips.Api.Constants;

namespace Project13.Tips.Api.Models
{
    public class CouponDto
    {
        public TipType TipType { get; set; }
        public int Week { get; set; }

        public long Turnover { get; set; }

        public List<MatchInfoDto> Matches { get; set; } = new();
    }
}
