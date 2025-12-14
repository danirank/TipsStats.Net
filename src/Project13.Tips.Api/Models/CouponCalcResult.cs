namespace Project13.Tips.Api.Models
{
    public sealed record CouponCalcResult
    (
      decimal FavOddsProduct, 
      int BigFavCount,
      int MedFavCount,
      int CloseMatchCount, 
      int TotalFavCount 

        
    );

    public sealed record MatchCalcResult(
    int MatchIndex,
    string FavoriteSign,      // "1", "X", "2"
    decimal FavoriteOdds,
    bool IsBigFavorite,
    bool IsCloseMatch);

}
