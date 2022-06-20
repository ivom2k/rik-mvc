namespace Domain.Base;
using Domain.Interfaces;

public abstract class BaseEntityId : BaseEntityId<Guid>, IBaseEntityId
{

}

public abstract class BaseEntityId<TKey> : IBaseEntityId<TKey> where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}
