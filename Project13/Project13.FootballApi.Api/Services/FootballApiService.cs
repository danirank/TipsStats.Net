using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Project13.FootballApi.Api.Settings;
using Project13.Stats.Core.Models;
using System.Text.Json;

namespace Project13.FootballApi.Api.Services
{
    public class FootballApiService
    {

        private readonly HttpClient _httpClient;

        private readonly string? _apiKey;

        private JsonSerializerOptions _jsonOptions;

        public FootballApiService(HttpClient httpClient, IOptions<FootballApiKeySettings> options)
        {
            _httpClient = httpClient;
            _apiKey = options.Value.ApiKey;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
        }

        [HttpGet]
        public async Task<TeamsApiResponse?> GetTeam(string teamName, string country)
        {
            var route = $"teams?name={teamName}&country={country}";

            var request = new HttpRequestMessage(
                HttpMethod.Get,route);

            request.Headers.Add("x-apisports-key", _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TeamsApiResponse>();
        }

        [HttpGet]
        public async Task<FixtureApiResponse?> GetFixture(string date, int teamId, int season)
        {
            var route = $"fixtures?season={season}&team={teamId}&date={date}";

            var request = new HttpRequestMessage(HttpMethod.Get, route);

            request.Headers.Add("x-apisports-key", _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<FixtureApiResponse>();
        }




        [HttpGet]
        public async Task<PredictionApiResponse?> GetPrediction(int fixtureId)
        {
            var route = $"predictions?fixture={fixtureId}";

            var request = new HttpRequestMessage(HttpMethod.Get, route);

            request.Headers.Add("x-apisports-key", _apiKey);

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<PredictionApiResponse>();
        }




    }
}
