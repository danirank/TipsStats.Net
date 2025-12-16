using Project13.Stats.Core.Models;

namespace Project13.Agent.Api.Models
{

    public sealed class RoundAnalysisResponse
    {
        public string? Summary { get; set; }
        public List<string> Bullets { get; set; } = new();
       
        public string response { get; set; } = string.Empty;
    }

    public sealed record RoundAnalasysReq (
           
        SearchAnalysisResult Stats

     );
}
