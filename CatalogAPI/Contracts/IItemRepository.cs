using CatalogAPI.Entities;
using System;
using System.Collections.Generic;

namespace CatalogAPI.Contracts
{
    public interface IItemsRepository
    {
        Item GetItem(Guid id);
        IEnumerable<Item> GetItems();
    }
}
