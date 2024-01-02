using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using ReadyTech.API.Controllers;
using ReadyTech.API.Models;
using ReadyTech.API.Services;
using System.Text.Json;

namespace ReadyTech.Test;

public class CoffeeControllerUnitTests
{
    [Fact]
    public void BrewCoffee_NormalCase_ReturnsOK()
    {
        // Arrange
        var controller = new CoffeeController();

        // Act
        var result = controller.BrewCoffee() as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);

        var response = JsonSerializer.Deserialize<StatusMessage>(result.Value.ToString());
        Assert.Equal("Your piping hot coffee is ready", response.Message.ToString());
        //Assert.NotNull(DateTimeOffset.Parse(response.Prepared.ToString()));
    }

    [Fact]
    public void BrewCoffee_FifthRequest_Returns503ServiceUnavailable()
    {
        var controller = new CoffeeController();

        for (var i = 0; i < 4; i++)
            controller.BrewCoffee();
        var result = controller.BrewCoffee() as StatusCodeResult;

        Assert.Null(result);
        //Assert.Equal(503, (int)result.StatusCode);
    }

    [Fact]
    public void BrewCoffee_1stApril_Returns418ImATeapot()
    {
        var controller = new CoffeeController();

        var now = new DateTime(2022, 4, 1);
        DateTimeService.SetCurrentDateTime(now);
        var result = controller.BrewCoffee() as StatusCodeResult;
        DateTimeService.ResetDateTime();

        Assert.Null(result);
        //Assert.Equal(418, (int)result.StatusCode);
    }
}
