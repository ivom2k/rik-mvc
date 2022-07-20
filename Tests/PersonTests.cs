namespace Tests;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;

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

    [Fact]
    public async void Test_Insert_Person_Returns_Created() {
        var person = new DTO.Public.Person()
        {
            FirstName = "Jüri",
            LastName = "Juurikas",
            PersonalIdentificationCode = "34501234215",
            Notes = "Test person"
        };

        var personDataInJson = JsonSerializer.Serialize(person);
        var personData = new StringContent(personDataInJson, Encoding.UTF8, "application/json");
        var personPostResponse = await _client.PostAsync("api/persons", personData);

        personPostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, personPostResponse.StatusCode);
    }

}