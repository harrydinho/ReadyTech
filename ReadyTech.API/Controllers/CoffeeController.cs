using Microsoft.AspNetCore.Mvc;
using ReadyTech.API.Models;
using ReadyTech.API.Services;
using System.Text.Json;

namespace ReadyTech.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeController : ControllerBase
{
    private static int requestCount = 0;

    private readonly IWeatherService weatherService;

    public CoffeeController(IWeatherService weatherService)
    {
        this.weatherService = weatherService;
    }

    [HttpGet("/brew-coffee")]
    public async Task<IActionResult> BrewCoffeeAsync()
    {
        // Check if it's 1st April
        if (DateTimeService.Now.Day == 1 && DateTimeService.Now.Month == 4)
            return StatusCode(418); // I'm a teapot

        requestCount++;

        // Check if every 5th call
        if (requestCount % 5 == 0)
            return StatusCode(503);

        var temperature = await weatherService.GetCurrentTemperatureAsync();

        // Make iced coffee if temperature is greater than 30 degrees
        if (temperature > 30)
        {
            var responseIcedCoffee = new StatusMessage
            {
                Message = "Your refreshing iced coffee is ready",
                Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
            };

            return Ok(JsonSerializer.Serialize(responseIcedCoffee));
        }

        // Normal case
        var responseHotCoffee = new StatusMessage
        {
            Message = "Your piping hot coffee is ready",
            Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };

        return Ok(JsonSerializer.Serialize(responseHotCoffee));
    }
}
