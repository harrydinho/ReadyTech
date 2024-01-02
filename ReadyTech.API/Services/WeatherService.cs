using ReadyTech.API.Models;

namespace ReadyTech.API.Services;

public interface IWeatherService
{
    Task<double> GetCurrentTemperatureAsync();
}

public class WeatherService : IWeatherService
{
    private readonly HttpClient httpClient;
    private readonly string weatherApiBaseUrl = "https://api.openweathermap.org/data/2.5/weather";
    private readonly string apiKey;

    public WeatherService(IConfiguration configuration, HttpClient httpClient)
    {
        this.httpClient = httpClient;
        this.httpClient.BaseAddress = new Uri(weatherApiBaseUrl);
        apiKey = configuration.GetValue<string>("WeatherApiKey");
    }

    public async Task<double> GetCurrentTemperatureAsync()
    {
        try
        {
            var weatherResponse = await httpClient.GetFromJsonAsync<WeatherResponse>($"?q=Sydney&appid={apiKey}");

            return weatherResponse?.Main?.Temperature - 273.15 ?? 0; // Return response - converting Kelvin to Celsius
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching weather data: {ex.Message}");
            return 0;
        }
    }
}
