using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{

    public class Detaljer
    {
        // Primärnyckel i databasen (kommer inte från CSV)
        [Ignore]
        public int Id { get; set; }
        

        [Name("produktnamn")]
        public string Produktnamn { get; set; } = string.Empty;

        [Name("omg")]
        public int Vecka { get; set; }          // t.ex. 202522?

        [Name("omgang")]
        public int Omg { get; set; }             // kortare omgångs-id

        [Name("matchnummer")]
        public int Matchnummer { get; set; }

        [Name("hemmalag")]
        public string Hemmalag { get; set; } = string.Empty;

        [Name("bortalag")]
        public string Bortalag { get; set; } = string.Empty;

        [Name("hemmaresultat")]
        public int? Hemmaresultat { get; set; }

        [Name("bortaresultat")]
        public int? Bortaresultat { get; set; }

        [Name("matchstart")]
        public DateTime? Matchstart { get; set; }

        [Name("utfall")]
        public string Utfall { get; set; } = string.Empty;      // "1", "X", "2", ev. tomt

        [Name("matchstatus")]
        public string Matchstatus { get; set; } = string.Empty; // t.ex. "played", "upcoming"

        [Name("experttips")]
        public string Experttips { get; set; } = string.Empty;  // t.ex. "1", "X", "2"

        [Name("oddset_high")]
        public decimal? OddsetHigh { get; set; }

        [Name("oddset_low")]
        public decimal? OddsetLow { get; set; }

        [Name("oddset_diff")]
        public decimal? OddsetDiff { get; set; }

        [Name("people_high")]
        public decimal? PeopleHigh { get; set; }

        [Name("people_low")]
        public decimal? PeopleLow { get; set; }

        [Name("people_diff")]
        public decimal? PeopleDiff { get; set; }

        [Name("people_rank")]
        public int? PeopleRank { get; set; }

        [Name("oddset_rank")]
        public int? OddsetRank { get; set; }

        [Name("oddset1")]
        public decimal? Oddset1 { get; set; }

        [Name("oddsetx")]
        public decimal? OddsetX { get; set; }

        [Name("oddset2")]
        public decimal? Oddset2 { get; set; }

        [Name("oddset_procent1")]
        public decimal? OddsetProcent1 { get; set; }

        [Name("oddset_procentx")]
        public decimal? OddsetProcentX { get; set; }

        [Name("oddset_procent2")]
        public decimal? OddsetProcent2 { get; set; }

        [Name("svenska_folket_odds1")]
        public decimal? SvenskaFolketOdds1 { get; set; }

        [Name("svenska_folket_oddsx")]
        public decimal? SvenskaFolketOddsX { get; set; }

        [Name("svenska_folket_odds2")]
        public decimal? SvenskaFolketOdds2 { get; set; }

        [Name("svenska_folket1")]
        public decimal? SvenskaFolket1 { get; set; }

        [Name("svenska_folketx")]
        public decimal? SvenskaFolketX { get; set; }

        [Name("svenska_folket2")]
        public decimal? SvenskaFolket2 { get; set; }

        [Name("tio_tidningar1")]
        public decimal? TioTidningar1 { get; set; }

        [Name("tio_tidningarx")]
        public decimal? TioTidningarX { get; set; }

        [Name("tio_tidningar2")]
        public decimal? TioTidningar2 { get; set; }

        [Name("people_was_right")]
        public int? PeopleWasRight { get; set; }

        [Name("oddset_was_right")]
        public int? OddsetWasRight { get; set; }

        [Name("people_was_wrong")]
        public int? PeopleWasWrong { get; set; }

        [Name("oddset_was_wrong")]
        public int? OddsetWasWrong { get; set; }

        [Name("expert_was_right")]
        public int? ExpertWasRight { get; set; }

        [Name("svspelinfo_id")]
        public int SvSpelInfoId { get; set; }    // unik/id från Svenska Spel


        [ForeignKey(nameof(SvSpelInfoId))]
        [Ignore]
        public Summering? Summering { get; set; }
    }


}
