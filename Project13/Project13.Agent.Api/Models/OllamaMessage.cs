



using System.Text.Json.Serialization;

namespace Project13.Agent.Api.Models
{

    //Det som skickas in till agenten. Exempel på roles: "system" - instruktion hur den ska svara. "user" -användare
    public sealed record Message(string role, string content); 


    public sealed class OllamaMessage
    {
        public string? Role { get; set; } 
        public string? Content { get; set; }
    }


    public sealed class OllamaResponse
    {
        public OllamaMessage? Message { get; set; }
    }


    //Hur jag vill att modellen ska svara
    public sealed class JsonAnswer
    {
        [JsonPropertyName("summary")]
        public string Summary { get; set; }

        [JsonPropertyName("bullets")]
        public List<string> Bullets  { get; set; }
    }



}