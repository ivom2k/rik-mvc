using AutoMapper;

public class AutoMapperRepositoryEntity : Profile
{
    public AutoMapperRepositoryEntity()
    {
        CreateMap<DTO.RepositoryEntity.Company, Domain.Models.Company>().ReverseMap();
        CreateMap<DTO.RepositoryEntity.Event, Domain.Models.Event>().ReverseMap();
        CreateMap<DTO.RepositoryEntity.Participation, Domain.Models.Participation>().ReverseMap();
        CreateMap<DTO.RepositoryEntity.PaymentType, Domain.Models.PaymentType>().ReverseMap();
        CreateMap<DTO.RepositoryEntity.Person, Domain.Models.Person>().ReverseMap();
    }
}