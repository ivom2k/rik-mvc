namespace Mappers.Configuration;

using AutoMapper;

public class AutoMapperPublicEntity : Profile
{
    public AutoMapperPublicEntity()
    {
        CreateMap<DTO.Public.Company, DTO.ServiceEntity.Company>().ReverseMap();
        CreateMap<DTO.Public.Event, DTO.ServiceEntity.Event>().ReverseMap();
        CreateMap<DTO.Public.Participation, DTO.ServiceEntity.Participation>().ReverseMap();
        CreateMap<DTO.Public.PaymentType, DTO.ServiceEntity.PaymentType>().ReverseMap();
        CreateMap<DTO.Public.Person, DTO.ServiceEntity.Person>().ReverseMap();
    }
}