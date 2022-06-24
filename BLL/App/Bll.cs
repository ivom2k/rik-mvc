namespace BLL.App;

using System.Threading.Tasks;
using BLL.Interfaces.App;
using BLL.Interfaces.Services;
using UnitOfWork.Interfaces;
using BLL.Services;
using Mappers.ServiceEntity;

public class Bll : BaseBll, IAppBll
{
    protected IAppUnitOfWork UnitOfWork;

    private readonly AutoMapper.IMapper _mapper;

    private ICompanyService? _companies;

    public ICompanyService Companies => _companies ??= new CompanyService(UnitOfWork.Companies, new CompanyMapper(_mapper));

    private IEventService? _events;

    public IEventService Events => _events ??= new EventService(UnitOfWork.Events, new EventMapper(_mapper));

    private IParticipationService? _participations;

    public IParticipationService Participations => _participations ??= new ParticipationService(UnitOfWork.Participations, new ParticipationMapper(_mapper));

    private IPaymentTypeService? _paymentTypes;

    public IPaymentTypeService PaymentTypes => _paymentTypes ??= new PaymentTypeService(UnitOfWork.PaymentTypes, new PaymentTypeMapper(_mapper));

    private IPersonService? _persons;

    public IPersonService Persons => _persons ??= new PersonService(UnitOfWork.Persons, new PersonMapper(_mapper));

    public Bll(IAppUnitOfWork unitOfWork, AutoMapper.IMapper mapper)
    {
        UnitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public override int SaveChanges()
    {
        return UnitOfWork.SaveChanges();
    }

    public override async Task<int> SaveChangesAsync()
    {
        return await UnitOfWork.SaveChangesAsync();
    }
}