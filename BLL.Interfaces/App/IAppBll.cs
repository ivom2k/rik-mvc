namespace BLL.Interfaces.App;
using BLL.Interfaces.Services;

public interface IAppBll : IBll
{
    ICompanyService Companies { get; }

    IEventService Events { get; }

    IParticipationService Participations { get; }

    IPaymentTypeService PaymentTypes { get; }

    IPersonService Persons { get; }

    public Task<DTO.ServiceEntity.Event> GetEventWithParticipantsCount(Guid id);
}