namespace UnitOfWork.Interfaces;
using Repositories.Interfaces.App;

public interface IAppUnitOfWork : IUnitOfWork {

    ICompanyRepository Companies { get; }

    IEventRepository Events { get; }

    IParticipationRepository Participations { get ; }

    IPaymentTypeRepository PaymentTypes { get; }

    IPersonRepository Persons { get; }
}