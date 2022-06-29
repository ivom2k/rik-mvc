using AutoMapper;
using BLL.Interfaces.App;
using Domain;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;
using Mappers.Configuration;
using UnitOfWork.Interfaces;
using UnitOfWork;
using BLL.App;

namespace Tests;

public class EventTests
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IAppUnitOfWork _uow;
    private readonly IAppBll _bll;

    public EventTests(ITestOutputHelper testOutputHelper)
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
}