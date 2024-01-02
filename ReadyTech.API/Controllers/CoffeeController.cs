using Microsoft.AspNetCore.Mvc;
using ReadyTech.API.Models;
using ReadyTech.API.Services;

namespace ReadyTech.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CoffeeController : ControllerBase
{
    private static int requestCount = 0;

    [HttpGet("/brew-coffee")]
    public IActionResult BrewCoffee()
    {
        requestCount++;

        // Check if it's 1st April
        if (DateTimeService.Now.Day == 1 && DateTimeService.Now.Month == 4)
            return StatusCode(418, null); // I'm a teapot
        //return StatusCode(418, "I'm a teapot");

        // Check if every 5th call
        if (requestCount % 5 == 0)
            return StatusCode(503, null);
            //return StatusCode(503, "Service Unavailable");

        var response = new StatusMessage
        {
            Message = "Your piping hot coffee is ready",
            Prepared = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:sszzz")
        };

        return Ok(response);
    }
}
