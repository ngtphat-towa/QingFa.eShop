using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    public interface IAggregateRoot<TEntityId> : IEntity<TEntityId> where TEntityId : notnull
    {
    }
}
