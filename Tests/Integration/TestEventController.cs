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
        var eventName = "Aktsionäride kokkutulek";

        var @event = new DTO.Public.Event()
        {
            Name = eventName,
            StartTime = DateTime.UtcNow,
            Location = "Tallinn",
            Notes = "Kohvi ei pakuta"
        };

        var paymentTypeCash = new DTO.Public.PaymentType()
        {
            Type = "Sularaha"
        };

        var person = new DTO.Public.Person()
        {
            FirstName = "Jüri",
            LastName = "Juurikas",
            PersonalIdentificationCode = "34501234215",
            Notes = "Test person"
        };

        var eventDataInJson = JsonSerializer.Serialize(@event);
        var eventData = new StringContent(eventDataInJson, Encoding.UTF8, "application/json");
        var eventPostResponse = await _client.PostAsync("api/events", eventData);

        var paymentTypeDataInJson = JsonSerializer.Serialize(paymentTypeCash);
        var paymentTypeData = new StringContent(paymentTypeDataInJson, Encoding.UTF8, "application/json");
        var paymentTypePostResponse = await _client.PostAsync("api/paymenttypes", paymentTypeData);

        var personDataInJson = JsonSerializer.Serialize(person);
        var personData = new StringContent(personDataInJson, Encoding.UTF8, "application/json");
        var personPostResponse = await _client.PostAsync("api/persons", personData);

        eventPostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, eventPostResponse.StatusCode);
        
        paymentTypePostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, paymentTypePostResponse.StatusCode);

        personPostResponse.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.Created, personPostResponse.StatusCode);

        var getEventResponseContent = await eventPostResponse.Content.ReadAsStringAsync();
        var eventFromResponseContent = JsonSerializer.Deserialize<DTO.Public.Event>(getEventResponseContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var getPaymentTypeResponseContent = await paymentTypePostResponse.Content.ReadAsStringAsync();
        var paymentTypeFromResponseContent = JsonSerializer.Deserialize<DTO.Public.PaymentType>(getEventResponseContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        var getPersonResponseContent = await paymentTypePostResponse.Content.ReadAsStringAsync();
        var personFromResponseContent = JsonSerializer.Deserialize<DTO.Public.Person>(getPersonResponseContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(eventFromResponseContent);
        Assert.NotNull(paymentTypeFromResponseContent);
        Assert.NotNull(personFromResponseContent);
      
        var eventId = eventFromResponseContent.Id;
        var paymentTypeId = paymentTypeFromResponseContent.Id;
        var personId = personFromResponseContent.Id;

        var participation = new DTO.Public.Participation()
        {
            EventId = eventId,
            PaymentTypeId = paymentTypeId,
            PersonId = personId
        };

        var participationDataInJson = JsonSerializer.Serialize(participation);
        var participationData = new StringContent(participationDataInJson, Encoding.UTF8, "application/json");

        var postParticipationRequest = new HttpRequestMessage();
        postParticipationRequest.Method = HttpMethod.Post;
        postParticipationRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        postParticipationRequest.Content = participationData;
        postParticipationRequest.RequestUri = new Uri("https://localhost:7194/api/participations");
        
        var postParticipationResponse = await _client.SendAsync(postParticipationRequest);
        postParticipationResponse.EnsureSuccessStatusCode();

        var getEventsRequest = new HttpRequestMessage();
        getEventsRequest.Method = HttpMethod.Get;
        getEventsRequest.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        getEventsRequest.RequestUri = new Uri($"https://localhost:7194/api/events/{eventId}");
        
        var getEventsResponse = await _client.SendAsync(getEventsRequest);
        getEventsResponse.EnsureSuccessStatusCode();

        var getEventsRequestContent = await getEventsResponse.Content.ReadAsStringAsync();
        var eventFromRequestContent = JsonSerializer.Deserialize<DTO.Public.Event>(getEventsRequestContent,
        new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        Assert.NotNull(eventFromRequestContent);
        Assert.Equal(1, eventFromRequestContent.TotalParticipants);
        Assert.Equal(eventName, eventFromRequestContent.Name);
    }
}