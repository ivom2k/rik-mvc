namespace Mappers;
using Mappers.Interfaces;

public class BaseMapper<TOut, TIn> : IBaseMapper<TOut, TIn>
{
    protected readonly AutoMapper.IMapper Mapper;

    public BaseMapper(AutoMapper.IMapper mapper)
    {
        Mapper = mapper;
    }

    public TOut? Map(TIn entity)
    {
        return Mapper.Map<TOut>(entity);
    }

    public TIn? Map(TOut? entity)
    {
        return Mapper.Map<TIn>(entity);
    }
}