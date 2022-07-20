namespace Tests.Integration;

using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

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
        var response = await _client.GetAsync("api/persons");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Test_Get_Persons_Returns_Empty_Array() {
        var getPersonsRequest = new HttpRequestMessage();
        getPersonsRequest.Method = HttpMethod.Get;
        getPersonsRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        getPersonsRequest.RequestUri = new Uri("https://localhost:7194/api/persons");
        
        var getPersonsResponse = await _client.SendAsync(getPersonsRequest);

        getPersonsResponse.EnsureSuccessStatusCode();

        var getPersonsRequestContent = await getPersonsResponse.Content.ReadAsStringAsync();
        var personsFromRequestContent = JsonSerializer.Deserialize<List<DTO.Public.Person>>(getPersonsRequestContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(personsFromRequestContent);
        Assert.Empty(personsFromRequestContent);
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

    [Fact]
    public async void Test_Insert_Person_And_Get_Persons() {
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

        var getPersonsRequest = new HttpRequestMessage();
        getPersonsRequest.Method = HttpMethod.Get;
        getPersonsRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        getPersonsRequest.RequestUri = new Uri("https://localhost:7194/api/persons");
        
        var getPersonsResponse = await _client.SendAsync(getPersonsRequest);

        getPersonsResponse.EnsureSuccessStatusCode();

        var getPersonsRequestContent = await getPersonsResponse.Content.ReadAsStringAsync();
        var personsFromRequestContent = JsonSerializer.Deserialize<List<DTO.Public.Person>>(getPersonsRequestContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(personsFromRequestContent);
        Assert.Single(personsFromRequestContent);
        Assert.NotNull(personsFromRequestContent[0]);
        Assert.NotNull(personsFromRequestContent[0].FullName);
        Assert.Equal("Jüri Juurikas", personsFromRequestContent[0].FullName);
    }
}