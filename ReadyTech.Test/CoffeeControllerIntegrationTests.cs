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
    }
}
