using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public class TeamRequestModel
    {
       public string? Country { get; set; } = string.Empty;
       public string? TeamName { get; set; } =string.Empty;

    }

    public class FixtureRequestModel
    {
        public int? TeamId { get; set; }

        public string? Date { get; set; } =string.Empty ;
    }

    public class PredictionRequestModel 
    {
        public int? FixtureId { get; set; }
    }

}
