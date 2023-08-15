using System;

namespace CatalogAPI.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
