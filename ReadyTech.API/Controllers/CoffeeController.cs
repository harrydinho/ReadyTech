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

    [HttpGet("/brew-coffee")]
    public IActionResult BrewCoffee()
    {
        // Check if it's 1st April
        if (DateTimeService.Now.Day == 1 && DateTimeService.Now.Month == 4)
            return StatusCode(418); // I'm a teapot

        requestCount++;

        // Check if every 5th call
        if (requestCount % 5 == 0)
            return StatusCode(503);

        var response = new StatusMessage
        {
            Message = "Your piping hot coffee is ready",
            Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };

        return Ok(JsonSerializer.Serialize(response));
    }
}
