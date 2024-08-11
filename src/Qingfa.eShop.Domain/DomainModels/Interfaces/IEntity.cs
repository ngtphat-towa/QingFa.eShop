using QingFa.EShop.Domain.DomainModels.Interfaces;

namespace QingFa.EShop.Domain.DomainModels
{
    /// <summary>
    /// Defines the base properties for an entity in the domain model.
    /// </summary>
    public interface IEntity<TEntityId> where TEntityId : notnull
    {
        TEntityId Id { get; }
    }
}
