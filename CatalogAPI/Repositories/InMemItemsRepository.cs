using CatalogAPI.Entities;
using System;
using System.Collections.Generic;

namespace CatalogAPI.Repositories
{
    public class InMemItemsRepository
    {
        private readonly List<Item> Items = new ()
        {
            new Item { Id = Guid.NewGuid(), Name = "", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "", Price = 10, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "", Price = 11, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "", Price = 13, CreatedDate = DateTimeOffset.UtcNow },
        };
    }
}
 