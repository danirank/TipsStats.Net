
using Project13.Agent.Api.Models;
using Project13.Agent.Api.Services;
using Project13.Stats.Core.Models;

namespace Project13.Agent.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var apiBaseAddress = new Uri("http://localhost:7255/");
            var ollamaBaseUri = new Uri("http://localhost:11434");

            builder.Services.AddHttpClient("Api", c =>
            {
                c.BaseAddress = apiBaseAddress;
            });

            builder.Services.AddScoped(sp =>
                sp.GetRequiredService<IHttpClientFactory>().CreateClient("Api"));

            builder.Services.AddScoped<AgentService>();

            builder.Services.AddSingleton(new OllamaAgentConfig("phi4-mini", ollamaBaseUri));

            builder.Services.AddHttpClient<OllamaAgentClient>((sp, c) =>
            {
                var cfg = sp.GetRequiredService<OllamaAgentConfig>();
                c.BaseAddress = cfg.BaseUri;
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("/test-ollama", async (AgentService agent, CancellationToken ct) =>
            {
                var raw = await agent.TestInstructionsAsync(ct);
                return Results.Ok(raw);
            });

            app.MapPost("/analyze", async (
                SearchAnalysisResult data,
             AgentService agent,
                CancellationToken ct) =>
            {
                var raw = await agent.AnalyzeAsync(data, ct);
                return Results.Ok(raw);
            });



            app.Run();
        }
    }
}
