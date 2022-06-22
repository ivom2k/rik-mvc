namespace UnitOfWork;
using UnitOfWork.Interfaces;
using Domain;
using Repositories.Interfaces.App;
using Repositories.App;
using Mappers.RepositoryEntity;

public class AppUnitOfWork : BaseUnitOfWork<ApplicationDbContext>, IAppUnitOfWork
{
    private readonly AutoMapper.IMapper _mapper;

    public AppUnitOfWork(ApplicationDbContext dbContext, AutoMapper.IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    private ICompanyRepository? _companies;

    public virtual ICompanyRepository Companies => _companies ??= new CompanyRepository(UnitOfWorkDbContext, new CompanyMapper(_mapper));

    private IEventRepository? _events;

    public virtual IEventRepository Events => _events ??= new EventRepository(UnitOfWorkDbContext, new EventMapper(_mapper));

    
    private IParticipationRepository? _participations;

    public IParticipationRepository Participations => _participations ??= new ParticipationRepository(UnitOfWorkDbContext, new ParticipationMapper(_mapper));

    
    private IPaymentTypeRepository? _paymentTypes;

    public IPaymentTypeRepository PaymentTypes => _paymentTypes ??= new PaymentTypeRepository(UnitOfWorkDbContext, new PaymentTypeMapper(_mapper));

    private IPersonRepository? _persons;

    public IPersonRepository Persons => _persons ??= new PersonRepository(UnitOfWorkDbContext, new PersonMapper(_mapper));
}