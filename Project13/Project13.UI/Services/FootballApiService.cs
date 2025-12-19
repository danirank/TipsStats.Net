using Project13.Stats.Core.Models;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Project13.UI.Services
{
    public sealed class FootballStatsApiService
    {
        private readonly HttpClient _http;

        public FootballStatsApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("FootballApi");
        }

        public async Task<TeamsApiResponse?> GetTeamAsync(string teamName, string country)
            => await _http.GetFromJsonAsync<TeamsApiResponse>(
                $"api/getTeam/?teamName={teamName}&country={country}");

        public async Task<FixtureApiResponse?> GetFixtureId(int? teamId, string date, int season)
            => await _http.GetFromJsonAsync<FixtureApiResponse>(
                $"api/getFixture/?date={date}&teamId={teamId}&season={season}");

        public async Task<PredictionApiResponse?> GetPrediction(int? fixtureId)
            => await _http.GetFromJsonAsync<PredictionApiResponse>(
                $"api/getPrediction/?fixtureId={fixtureId}");


        public async Task<int?> TeamId(string teamName, string country)
        {
            try
            {
                var data = await GetTeamAsync(teamName, country);
                int? teamId = data?.Response?.Select(x => (int?)x.Team.Id).FirstOrDefault();
                Console.WriteLine("TeamId: " + teamId );
                return teamId;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting teamId " + ex.Message);
                return null;
            }
        }

        public async Task<int?> FixtureId(int? teamId, string date, int season)
        {
            try
            {
                Console.WriteLine("TeamId: "+teamId+"\nDate: "+date+"\nSeason: "+season);

                var data = await GetFixtureId(teamId, date, season);
                int? fixtureId = data?.Response?.Select(x => (int?)x.Fixture.Id).FirstOrDefault();

                Console.WriteLine("fixtureId: " +fixtureId);
                return fixtureId;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error getting FixtureID " + ex.Message);
                return null;
            }
        }


        public async Task<string?> Prediction(int? fixtureId)
        {
            try
            {

            var data = await GetPrediction(fixtureId);

            var prediction = data?.Response.Select(x => x.Prediction).FirstOrDefault();

            var winner = prediction?.Winner?.Name;
            var comment = prediction?.Winner?.Comment;
            var advice = prediction?.Advice;

            var percent = prediction?.Percent;

            var pct1 = percent?.Percent1;
            var pctX = percent?.PercentX;
            var pct2 = percent?.Percent2;


            return $"Prediction: {winner} - {comment}\nAdvice: {advice}\nProbobility\n  Home Win: {pct1}\nDraw: {pctX}\nAway win: {pct2}";

            } 
            catch (Exception ex )
            {
                Console.WriteLine("Error getting prediction " + ex.Message);
                return null;
            }

        }

        public async Task<string?> LoadPrediction(string teamName, string country, string date, int season)
        {
            try
            {
            var teamId = await TeamId(teamName, country);
            var fixtureId = await FixtureId(teamId, date, season);
            return await Prediction(fixtureId);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong when loading all methods "+ex.Message);
            }
            return null;

        }

    }

}
