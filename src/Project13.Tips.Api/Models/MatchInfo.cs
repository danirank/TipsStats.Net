using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Tips.Api.Models
{

    public class MatchInfo
    {
        public int Matchnummer { get; set; }
        public string Hemmalag { get; set; } = string.Empty;
        public string Bortalag { get; set; } = string.Empty;
        public double Odds1 { get; set; }
        public double OddsX { get; set; }
        public double Odds2 { get; set; }
        public double SvenskaFolketOdds1 => BeräknaProcentTillOdds(SvenskaFolketProcent1);
        public double SvenskaFolketOddsX => BeräknaProcentTillOdds(SvenskaFolketProcentX);
        public double SvenskaFolketOdds2 => BeräknaProcentTillOdds(SvenskaFolketProcent2);
        public double OddsProcent1 => OddsTillProcent(Odds1);
        public double OddsProcentX => OddsTillProcent(OddsX);
        public double OddsProcent2 => OddsTillProcent(Odds2);
        public double SvenskaFolketProcent1 { get; set; }
        public double SvenskaFolketProcentX { get; set; }
        public double SvenskaFolketProcent2 { get; set; }
        public double Kvot1 => BeräknaKvot(SvenskaFolketProcent1, Odds1);
        public double KvotX => BeräknaKvot(SvenskaFolketProcentX, OddsX);
        public double Kvot2 => BeräknaKvot(SvenskaFolketProcent2, Odds2);





        private double BeräknaKvot(double procent, double odds)
        {
            if (procent <= 0 || odds <= 0)
            {
                return 0;
            }

            var folketOdds = 100.0 / procent;
            return folketOdds / odds;
        }

        private double OddsTillProcent(double odds)
        {
            if (odds <= 0)
            {
                return 0;
            }
            return Math.Round(100.0 / odds);
        }

        private double BeräknaProcentTillOdds(double procent)
        {
            if (procent <= 0)
            {
                return 0;
            }
            return Math.Round(100.0 / procent, 2);
        }

    }




}
