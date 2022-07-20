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

    [Fact]
    public async void Test_Get_Events_Returns_Ok() {
        var response = await _client.GetAsync("api/events");

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async void Test_Insert_Event_Returns_Created() {
        var @event = new DTO.Public.Event()
        {
            Name = "Aktsionäride kokkutulek",
            StartTime = DateTime.UtcNow,
            Location = "Tallinn",
            Notes = "Kohvi ei pakuta"
        };

        var eventDataInJson = JsonSerializer.Serialize(@event);
        var eventData = new StringContent(eventDataInJson, Encoding.UTF8, "application/json");
        var eventPostResponse = await _client.PostAsync("api/events", eventData);

        eventPostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, eventPostResponse.StatusCode);
    }

    [Fact]
    public async void Test_Insert_Event_And_Add_A_Person() {
        var @event = new DTO.Public.Event()
        {
            Name = "Aktsionäride kokkutulek",
            StartTime = DateTime.UtcNow,
            Location = "Tallinn",
            Notes = "Kohvi ei pakuta"
        };

        var paymentTypeCash = new DTO.Public.PaymentType()
        {
            Type = "Sularaha"
        };

        var eventDataInJson = JsonSerializer.Serialize(@event);
        var eventData = new StringContent(eventDataInJson, Encoding.UTF8, "application/json");
        var eventPostResponse = await _client.PostAsync("api/events", eventData);

        var paymentTypeDataInJson = JsonSerializer.Serialize(paymentTypeCash);
        var paymentTypeData = new StringContent(paymentTypeDataInJson, Encoding.UTF8, "application/json");
        var paymentTypePostResponse = await _client.PostAsync("api/paymenttypes", paymentTypeData);

        eventPostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, eventPostResponse.StatusCode);
        
        paymentTypePostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, paymentTypePostResponse.StatusCode);

        var getEventResponseContent = await eventPostResponse.Content.ReadAsStringAsync();
        var eventFromResponseContent = JsonSerializer.Deserialize<DTO.Public.Event>(getEventResponseContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var getPaymentTypeResponseContent = await paymentTypePostResponse.Content.ReadAsStringAsync();
        var paymentTypeFromResponseContent = JsonSerializer.Deserialize<DTO.Public.PaymentType>(getEventResponseContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(eventFromResponseContent);
        Assert.NotNull(paymentTypeFromResponseContent);

        var eventId = eventFromResponseContent.Id;
        var paymentTypeId = paymentTypeFromResponseContent.Id;



    }

}