namespace Mappers.Interfaces;
public interface IBaseMapper<TOut, TIn>
{
    TOut? Map(TIn? entity);

    TIn? Map(TOut entity);
}