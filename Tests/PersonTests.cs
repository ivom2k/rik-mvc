namespace Tests;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

public class TestPersonController : IClassFixture<CustomWebApplicationFactory<Program>> {
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public TestPersonController(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            }
        );
    }

    [Fact]
    public async void Test_Get_Persons_Returns_Ok() {
        var response = await _client.GetAsync("api/Persons");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}