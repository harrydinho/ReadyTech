using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Moq;
using ReadyTech.API.Controllers;
using ReadyTech.API.Models;
using ReadyTech.API.Services;
using System.Text.Json;

namespace ReadyTech.Test;

public class CoffeeControllerUnitTests
{
    [Fact]
    public async Task BrewCoffee_NormalCase_ReturnsHotCoffee()
    {
        // Arrange
        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock.Setup(x => x.GetCurrentTemperatureAsync()).ReturnsAsync(25);
        
        var controller = new CoffeeController(weatherServiceMock.Object);

        // Act
        var result = await controller.BrewCoffeeAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);

        var response = ((OkObjectResult)result).Value.ToString();
        var responseObject = JsonSerializer.Deserialize<StatusMessage>(response);
        Assert.Equal("Your piping hot coffee is ready", responseObject.Message);
    }

    [Fact]
    public async Task BrewCoffee_NormalCase_ReturnsIcedCoffee()
    {
        // Arrange
        var weatherServiceMock = new Mock<IWeatherService>();
        weatherServiceMock.Setup(x => x.GetCurrentTemperatureAsync()).ReturnsAsync(35);

        var controller = new CoffeeController(weatherServiceMock.Object);

        // Act
        var result = await controller.BrewCoffeeAsync();

        // Assert
        Assert.NotNull(result);
        Assert.IsType<OkObjectResult>(result);

        var response = ((OkObjectResult)result).Value.ToString();
        var responseObject = JsonSerializer.Deserialize<StatusMessage>(response);
        Assert.Equal("Your refreshing iced coffee is ready", responseObject.Message);
    }

    [Fact]
    public async Task BrewCoffee_FifthRequest_Returns503ServiceUnavailable()
    {
        var weatherServiceMock = new Mock<IWeatherService>();
        var controller = new CoffeeController(weatherServiceMock.Object);

        for (var i = 0; i < 4; i++)
            await controller.BrewCoffeeAsync();
        var result = await controller.BrewCoffeeAsync();

        Assert.NotNull(result);
        Assert.IsType<StatusCodeResult>(result);

        var statusCode = ((StatusCodeResult)result).StatusCode;
        Assert.Equal(503, statusCode);
    }

    [Fact]
    public async void BrewCoffee_1stApril_Returns418ImATeapot()
    {
        var weatherServiceMock = new Mock<IWeatherService>();
        var controller = new CoffeeController(weatherServiceMock.Object);

        var now = new DateTime(2022, 4, 1);
        DateTimeService.SetCurrentDateTime(now);
        var result = await controller.BrewCoffeeAsync();
        DateTimeService.ResetDateTime();

        Assert.NotNull(result);
        Assert.IsType<StatusCodeResult>(result);

        var statusCode = ((StatusCodeResult)result).StatusCode;
        Assert.Equal(418, statusCode);
    }
}
