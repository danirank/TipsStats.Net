namespace Project13.Stats.Core.Models
{
    public class MatchDto
    {
        public int Matchnummer { get; set; }
        public string Hemmalag { get; set; } = "";
        public string Bortalag { get; set; } = "";
        public string Utfall { get; set; } = "";

        public decimal? Oddset1 { get; set; }
        public decimal? OddsetX { get; set; }
        public decimal? Oddset2 { get; set; }

        public decimal? SvenskaFolket1 { get; set; }
        public decimal? SvenskaFolketX { get; set; }
        public decimal? SvenskaFolket2 { get; set; }

        public decimal Disagreement { get; set; }      // |folket% - odds%|
    }
}
