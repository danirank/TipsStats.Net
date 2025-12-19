namespace Project13.Tips.Api.Constants
{
    public enum TipType
    {
        Stryktipset,
        Europatipset,
        Topptipset
    }
    public static class TipsNamn
    {
        public const string Stryktipset = "stryktipset";
        public const string Europatipset = "europatipset";
        public const string Topptipset = "topptipset";
    }

    public static class ApiRoutes
    {
        public const string Base = "api/svenskaspel?tipsTyp=";
        public const string Stryktipset = Base + TipsNamn.Stryktipset;
        public const string Europatipset = Base + TipsNamn.Europatipset;
        public const string Topptipset = Base + TipsNamn.Topptipset;
    }

    
}
