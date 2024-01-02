using Microsoft.AspNetCore.Mvc.Testing;
using ReadyTech.API.Models;
using System.Text.Json;

namespace ReadyTech.Test;

public class CoffeeControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> factory;

    public CoffeeControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task BrewCoffee_NormalCase_ReturnsOk()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/brew-coffee");
        var result = await response.Content.ReadAsStringAsync();

        Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);

        var responseObject = JsonSerializer.Deserialize<StatusMessage>(result);
        Assert.Equal("Your piping hot coffee is ready", responseObject.Message.ToString());
    }

    [Fact]
    public async Task BrewCoffee_FifthRequest_Returns503ServiceUnavailable()
    {
        var client = factory.CreateClient();

        for (int i = 0; i < 4; i++)
            await client.GetAsync("/brew-coffee");
        var response = await client.GetAsync("/brew-coffee");

        Assert.Equal(System.Net.HttpStatusCode.ServiceUnavailable, response.StatusCode);
    }

    [Fact]
    public async Task BrewCoffee_1stApril_Returns418ImATeapot()
    {
        var client = factory.CreateClient();
        var response = await client.GetAsync("/brew-coffee");
        Assert.Equal(418, (int)response.StatusCode);
    }
}
