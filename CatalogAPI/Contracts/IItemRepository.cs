﻿using CatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Contracts
{
    public interface IItemsRepository
    {
        Task<Item> GetItemAsync(Guid id);
        Task<IReadOnlyCollection<Item>> GetAllItemsAsync();
        Task CreateItemAsync(Item item);
        Task UpdateItemAsync(Item item);
        Task DeleteItemAsync(Guid id);
    }
}
