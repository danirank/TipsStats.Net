
using Project13.FootballApi.Api.Services;
using Project13.FootballApi.Api.Settings;

namespace Project13.FootballApi.Api
{
    using Project13.FootballApi.Api.Services;
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var apiKey = builder.Configuration["FootballApiKey"];

            builder.Services.Configure<FootballApiKeySettings>(builder.Configuration.GetSection("FootballApiKey"));

            var baseAdress = new Uri("https://v3.football.api-sports.io/");

            builder.Services.AddHttpClient<FootballApiService>(c => c.BaseAddress = baseAdress);


            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapGet("api/getTeam/", async (string teamName, string country, FootballApiService service) =>
            {
                var result = await service.GetTeam(teamName, country);
                return Results.Ok(result);
            });

            app.MapGet("api/getFixture/", async (string date, int teamId, int season, FootballApiService service) =>
            {
                var result = await service.GetFixture(date, teamId,season);
                return Results.Ok(result);
            }); 
            app.MapGet("api/getPrediction/", async (int fixtureId, FootballApiService service) =>
            {
                var result = await service.GetPrediction(fixtureId);
                return Results.Ok(result);
            });


            app.Run();
        }
    }
}
