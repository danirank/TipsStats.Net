using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{

    public class Summering
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Name("id")]
        public int Id { get; set; }

        public List<Detaljer> Matches { get; set; } = new();

        [Name("omg")]
        public int Vecka { get; set; }

        [Name("produktnamn")]
        public string Produktnamn { get; set; } = string.Empty;

        [Name("correct_row")]
        public string CorrectRow { get; set; } = string.Empty;

        [Name("turnover")]
        public decimal Turnover { get; set; }

        [Name("utd13")]
        public decimal? Utd13 { get; set; }

        [Name("utd12")]
        public decimal? Utd12 { get; set; }

        [Name("utd11")]
        public decimal? Utd11 { get; set; }

        [Name("utd10")]
        public decimal? Utd10 { get; set; }

        [Name("ant13")]
        public int? Ant13 { get; set; }

        [Name("ant12")]
        public int? Ant12 { get; set; }

        [Name("ant11")]
        public int? Ant11 { get; set; }

        [Name("ant10")]
        public int? Ant10 { get; set; }

        [Name("count1")]
        public int? Count1 { get; set; }

        [Name("countx")]
        public int? CountX { get; set; }

        [Name("count2")]
        public int? Count2 { get; set; }

        [Name("random_results")]
        public int? RandomResults { get; set; }

        [Name("people_was_right")]
        public int? PeopleWasRight { get; set; }

        [Name("oddset_was_right")]
        public int? OddsetWasRight { get; set; }

        [Name("people_was_wrong")]
        public int? PeopleWasWrong { get; set; }

        [Name("oddset_was_wrong")]
        public int? OddsetWasWrong { get; set; }

        [Name("people_max_sum")]
        public decimal? PeopleMaxSum { get; set; }

        [Name("oddset_max_sum")]
        public decimal? OddsetMaxSum { get; set; }

        [Name("people_min_sum")]
        public decimal? PeopleMinSum { get; set; }

        [Name("oddset_min_sum")]
        public decimal? OddsetMinSum { get; set; }

        [Name("people_sum")]
        public decimal? PeopleSum { get; set; }

        [Name("oddset_sum")]
        public decimal? OddsetSum { get; set; }

        [Name("people_diff_sum")]
        public decimal? PeopleDiffSum { get; set; }

        [Name("oddset_diff_sum")]
        public decimal? OddsetDiffSum { get; set; }

        [Name("odds1_ok")]
        public decimal? Odds1Ok { get; set; }

        [Name("odds2_ok")]
        public decimal? Odds2Ok { get; set; }

        [Name("odds3_ok")]
        public decimal? Odds3Ok { get; set; }

        [Name("people1_ok")]
        public decimal? People1Ok { get; set; }

        [Name("people2_ok")]
        public decimal? People2Ok { get; set; }

        [Name("people3_ok")]
        public decimal? People3Ok { get; set; }

        
    }


}
