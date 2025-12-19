using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Project13.Stats.Core.Models
{
    public sealed record TeamsApiResponse
    {
        [JsonPropertyName("response")]
        public List<TeamWrapper> Response { get; init; } = [];
    }

    public sealed record TeamWrapper
    {
        [JsonPropertyName("team")]
        public TeamResponseModel Team { get; init; } = default!;
    }

    public sealed record TeamResponseModel
    {
        [JsonPropertyName("id")]
        public int? Id { get; init; }

        [JsonPropertyName("name")]
        public string? Name { get; init; } = string.Empty;

    }

    public sealed record FixtureApiResponse
    {
        [JsonPropertyName("response")]
        public List<FixtureResponseWrapper> Response { get; init; } = [];
    }
    public sealed record FixtureResponseWrapper
    {
        [JsonPropertyName("fixture")]
        public FixtureResponseModel Fixture { get; init; } = default!;


    }
    public sealed record FixtureResponseModel
    {
        [JsonPropertyName("id")]
        public int? Id { get; init; }

    }

    public sealed record PredictionApiResponse
    {
        [JsonPropertyName("response")]
        public List<PredictionResponseWrapper> Response { get; init; } = [];
    }
    public sealed record PredictionResponseWrapper
    {
        [JsonPropertyName("predictions")]
        public PredictionResponseModel Prediction { get; init; } = default!;

    }
    public sealed record PredictionResponseModel
    {
        [JsonPropertyName("winner")]
        public WinnerModel? Winner { get; init; }

        [JsonPropertyName("win_or_draw")]
        public bool? IsWinOrDraw { get; init; }

        [JsonPropertyName("percent")]
        public PercentModel? Percent { get; init; }

        [JsonPropertyName("advice")]
        public string? Advice { get; init; } 
    }

    public sealed record WinnerModel
    {
        [JsonPropertyName("name")]
        public string? Name { get; init; }

        [JsonPropertyName("comment")]
        public string? Comment { get; init; } = string.Empty;
    }

    public sealed record PercentModel
    {
        [JsonPropertyName("home")]
        public string? Percent1 { get; init; }
        [JsonPropertyName("draw")]
        public string? PercentX { get; init; }

        [JsonPropertyName("away")]
        public string? Percent2 { get; init; }


    }


}
