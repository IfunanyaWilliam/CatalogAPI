using CatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Contracts
{
    public interface IRepository<T> where T : IEntity
    {
        Task<T> GetItemAsync(Guid id);
        Task<IReadOnlyCollection<T>> GetAllItemsAsync();
        Task CreateItemAsync(T item);
        Task UpdateItemAsync(T item);
        Task DeleteItemAsync(Guid id);
    }
}
