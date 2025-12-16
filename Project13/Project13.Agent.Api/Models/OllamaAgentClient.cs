using Microsoft.AspNetCore.Authorization.Infrastructure;
using Project13.Agent.Api.Prompts;
namespace Project13.Agent.Api.Models
{
    public class OllamaAgentClient
    {
        private readonly HttpClient _httpClient;
        private readonly OllamaAgentConfig _config;
       

        public OllamaAgentClient(HttpClient http, OllamaAgentConfig config)
        {
            _httpClient = http;
            _config = config;
        }



        public async Task<string> ChatAsync(string prompt, CancellationToken ct = default)
        {
            var request = new
            {
                model = _config.ModelName,
                stream = false,
                messages = new[]
                {
                    new
                    {
                        role = Roles.User,
                        content = prompt,
                    }
                }

            };


            using var response = await _httpClient.PostAsJsonAsync("/api/chat", request, ct); 

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync(ct);

            return result ?? string.Empty;
        }



    }

    public sealed record OllamaAgentConfig(
        string ModelName,
        Uri BaseUri
        );
}
