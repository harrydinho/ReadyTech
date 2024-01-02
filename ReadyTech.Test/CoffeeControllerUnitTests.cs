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
        var result = controller.BrewCoffee();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);

        var response = ((OkObjectResult)result).Value.ToString();
        var responseObject = JsonSerializer.Deserialize<StatusMessage>(response);
        Assert.Equal("Your piping hot coffee is ready", responseObject.Message);
        Assert.NotNull(DateTimeOffset.Parse(responseObject.Prepared));
    }

    [Fact]
    public void BrewCoffee_FifthRequest_Returns503ServiceUnavailable()
    {
        var controller = new CoffeeController();

        for (var i = 0; i < 4; i++)
            controller.BrewCoffee();
        var result = controller.BrewCoffee();

        Assert.NotNull(result);
        Assert.IsType<StatusCodeResult>(result);

        var statusCode = ((StatusCodeResult)result).StatusCode;
        Assert.Equal(503, statusCode);
    }

    [Fact]
    public void BrewCoffee_1stApril_Returns418ImATeapot()
    {
        var controller = new CoffeeController();

        var now = new DateTime(2022, 4, 1);
        DateTimeService.SetCurrentDateTime(now);
        var result = controller.BrewCoffee();
        DateTimeService.ResetDateTime();

        Assert.NotNull(result);
        Assert.IsType<StatusCodeResult>(result);

        var statusCode = ((StatusCodeResult)result).StatusCode;
        Assert.Equal(418, statusCode);
    }
}
