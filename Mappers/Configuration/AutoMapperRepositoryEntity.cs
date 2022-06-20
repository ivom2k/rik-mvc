using AutoMapper;

public class AutoMapperRepositoryEntity : Profile
{
    public AutoMapperRepositoryEntity()
    {
        CreateMap<DTO.RepositoryEntity.Company, Domain.Models.Company>().ReverseMap();
        CreateMap<DTO.RepositoryEntity.Event, Domain.Models.Event>().ReverseMap();
    }
}