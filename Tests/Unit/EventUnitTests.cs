using AutoMapper;
using BLL.Interfaces.App;
using Domain;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Mappers.Configuration;
using UnitOfWork.Interfaces;
using UnitOfWork;
using BLL.App;
using Mappers.PublicEntity;

namespace Tests.Unit;

public class EventUnitTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
    private readonly IAppBll _bll;

    public EventUnitTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());

        _context = new ApplicationDbContext(optionsBuilder.Options);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();

        var mockMapper = new MapperConfiguration(configuration => {
            configuration.AddProfile<AutoMapperRepositoryEntity>();
            configuration.AddProfile<AutoMapperServiceEntity>();
            configuration.AddProfile<AutoMapperPublicEntity>();
        });

        _mapper = mockMapper.CreateMapper();
        _uow = new AppUnitOfWork(_context, _mapper);
        _bll = new Bll(_uow, _mapper);
    }

    [Fact]
    public async void Test_Events_Should_Be_Empty()
    {
        var events = await _bll.Events.GetAllAsync();

        Assert.NotNull(events);
        Assert.Empty(events);
    }

    [Fact]
    public async void Test_Events_Can_Add_An_Event()
    {
        var @event = new DTO.ServiceEntity.Event
        {
            Name = "Test event",
            StartTime = DateTime.UtcNow,
            Location = "Testland",
            Notes = "Test notes"
        };

        _bll.Events.Add(@event);
        await _bll.SaveChangesAsync();
        var result = await _bll.Events.GetAllAsync();

        Assert.NotNull(result);
        Assert.Single(result);
    }

    [Fact]
    public async void Test_Event_Returns_Correct_Partipation_Count_For_Single_Person()
    {
        var eventMapper = new EventMapper(_mapper);

        var @event = new DTO.ServiceEntity.Event
        {
            Name = "Test event",
            StartTime = DateTime.UtcNow,
            Location = "Testland",
            Notes = "Test notes"
        };

        var person = new DTO.ServiceEntity.Person
        {
            FirstName = "TesFirstName",
            LastName = "TestLastName",
            PersonalIdentificationCode = "34501234215",
            Notes = "TestNotes"
        };

        var paymentType = new DTO.ServiceEntity.PaymentType
        {
            Type = "Kaardimakse"
        };

        var eventId = _bll.Events.Add(@event).Id;
        var personId = _bll.Persons.Add(person).Id;
        var paymentTypeId = _bll.PaymentTypes.Add(paymentType).Id;
        
        var participation = new DTO.ServiceEntity.Participation
        {
            EventId = eventId,
            PersonId = personId,
            PaymentTypeId = paymentTypeId
        };
        
        _bll.Participations.Add(participation);
        await _bll.SaveChangesAsync();
        
        var result = eventMapper.Map(await _bll.GetEventWithParticipantsCount(eventId));       

        Assert.NotNull(result);
        Assert.IsType<DTO.Public.Event>(result);
        Assert.Equal(1, result.TotalParticipants);
    }

    [Fact]
    public async void Test_Event_Returns_Correct_Partipation_Count_For_Company_People()
    {
        var eventMapper = new EventMapper(_mapper);

        var @event = new DTO.ServiceEntity.Event
        {
            Name = "Test event",
            StartTime = DateTime.UtcNow,
            Location = "Testland",
            Notes = "Test notes"
        };

        var company = new DTO.ServiceEntity.Company
        {
            Name = "Test company",
            Code = 77000312,
            ParticipantsCount = 12,
            Notes = "Accountants department"
        };

        var paymentType = new DTO.ServiceEntity.PaymentType
        {
            Type = "Sularaha"
        };

        var eventId = _bll.Events.Add(@event).Id;
        var companyId = _bll.Companies.Add(company).Id;
        var paymentTypeId = _bll.PaymentTypes.Add(paymentType).Id;

        var participation = new DTO.ServiceEntity.Participation
        {
            EventId = eventId,
            CompanyId = companyId,
            PaymentTypeId = paymentTypeId
        };

        _bll.Participations.Add(participation);
        await _bll.SaveChangesAsync();
        
        var result = eventMapper.Map(await _bll.GetEventWithParticipantsCount(eventId));

        Assert.NotNull(result);
        Assert.IsType<DTO.Public.Event>(result);
        Assert.Equal(12, result.TotalParticipants);
    }

    [Fact]
    public async void Test_Event_Returns_Correct_Partipation_Count_For_Company_People_And_Person()
    {
        var eventMapper = new EventMapper(_mapper);

        var @event = new DTO.ServiceEntity.Event
        {
            Name = "Test event",
            StartTime = DateTime.UtcNow,
            Location = "Testland",
            Notes = "Test notes"
        };

        var company = new DTO.ServiceEntity.Company
        {
            Name = "Test company",
            Code = 77000312,
            ParticipantsCount = 2,
            Notes = "Accountants department"
        };

        var paymentType = new DTO.ServiceEntity.PaymentType
        {
            Type = "Sularaha"
        };

        var person = new DTO.ServiceEntity.Person
        {
            FirstName = "TesFirstName",
            LastName = "TestLastName",
            PersonalIdentificationCode = "34501234215",
            Notes = "TestNotes"
        };

        var eventId = _bll.Events.Add(@event).Id;
        var companyId = _bll.Companies.Add(company).Id;
        var personId = _bll.Persons.Add(person).Id;
        var paymentTypeId = _bll.PaymentTypes.Add(paymentType).Id;

        var personParticipation = new DTO.ServiceEntity.Participation
        {
            EventId = eventId,
            PersonId = personId,
            PaymentTypeId = paymentTypeId
        };

        var companyParticipation = new DTO.ServiceEntity.Participation
        {
            EventId = eventId,
            CompanyId = companyId,
            PaymentTypeId = paymentTypeId
        };

        _bll.Participations.Add(personParticipation);
        _bll.Participations.Add(companyParticipation);

        await _bll.SaveChangesAsync();

        var result = eventMapper.Map(await _bll.GetEventWithParticipantsCount(eventId));

        Assert.NotNull(result);
        Assert.IsType<DTO.Public.Event>(result);
        Assert.Equal(3, result.TotalParticipants);
    }
}