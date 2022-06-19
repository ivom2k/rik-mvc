public interface IBaseEntityId : IBaseEntityId<Guid> {
    
}

public interface IBaseEntityId<TKey> where TKey : IBaseEntityId<TKey> {
    public TKey Id { get; set; }
}