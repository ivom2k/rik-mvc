namespace Mappers.Configuration;

using AutoMapper;

public class AutoMapperServiceEntity : Profile
{
    public AutoMapperServiceEntity()
    {
        CreateMap<DTO.ServiceEntity.Company, DTO.RepositoryEntity.Company>().ReverseMap();
        CreateMap<DTO.ServiceEntity.Event, DTO.RepositoryEntity.Event>().ReverseMap();
        CreateMap<DTO.ServiceEntity.Participation, DTO.RepositoryEntity.Participation>().ReverseMap();
        CreateMap<DTO.ServiceEntity.PaymentType, DTO.RepositoryEntity.PaymentType>().ReverseMap();
        CreateMap<DTO.ServiceEntity.Person, DTO.RepositoryEntity.Person>().ReverseMap();
    }
}