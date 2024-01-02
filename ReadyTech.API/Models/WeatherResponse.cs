using System.Text.Json.Serialization;

namespace ReadyTech.API.Models;

public class WeatherResponse
{
    public MainData Main { get; set; }

    public class MainData
    {
        [JsonPropertyName("temp")]
        public double Temperature { get; set; }
    }
}
