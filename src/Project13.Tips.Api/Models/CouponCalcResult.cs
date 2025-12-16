namespace Project13.Tips.Api.Models
{
    public sealed record CouponCalcResult
    (
      decimal FavOddsProduct, 
      int BigFavCount,
      int MedFavCount,
      int CloseMatchCount, 
      int TotalFavCount,
      decimal DisagreementTop3Sum  

        
    );

    public sealed record MatchCalcResult
        (
                int MatchIndex,
                string FavoriteSign,      
                decimal FavoriteOdds,
                bool IsBigFavorite,
                bool IsCloseMatch,
               decimal Disagreement
        );

}
