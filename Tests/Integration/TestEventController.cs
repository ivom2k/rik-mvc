namespace Tests.Integration;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

public class TestEventController : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly CustomWebApplicationFactory<Program> _factory;

    public TestEventController(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient(
            new WebApplicationFactoryClientOptions()
            {
                AllowAutoRedirect = false
            }
        );
    }

}