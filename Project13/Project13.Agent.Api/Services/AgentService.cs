using Project13.Agent.Api.Models;
using Project13.Agent.Api.Prompts;
using Project13.Stats.Core.Models;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Project13.Agent.Api.Services
{
    public class AgentService
    {
        private readonly OllamaAgentClient _ollama;
        private readonly JsonSerializerOptions _json = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public AgentService(OllamaAgentClient client)
        {
            _ollama = client;
        }


        public Task<string> TestInstructionsAsync(CancellationToken ct = default)
        {
            // just nu testar vi din prompt rakt av
            return _ollama.ChatAsync(PromptTemplates.Instructions, ct);
        }

        public Task<string> AnalyzeAsync(SearchAnalysisResult data, CancellationToken ct = default)
        {
            var dataJson = JsonSerializer.Serialize(data, _json);

            var prompt =
                $""""
                {PromptTemplates.Instructions}

                Task:
                Analyze the provided round statistics and summarize the key signals.

                Input data:
                """
                {dataJson}
                """

                Return a concise answer.
               
                """";

            return _ollama.ChatAsync(prompt, ct);
        }


    }
    


}
