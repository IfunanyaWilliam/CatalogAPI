using CatalogAPI.Contracts;
using CatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogAPI.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        private readonly List<Item> Items = new()
        {
            new Item { Id = Guid.NewGuid(), Name = "Potion", Price = 9, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Iron Sword", Price = 20, CreatedDate = DateTimeOffset.UtcNow },
            new Item { Id = Guid.NewGuid(), Name = "Bronze Shield", Price = 18, CreatedDate = DateTimeOffset.UtcNow }
        };


        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            return await Task.FromResult(Items);
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var item =  Items.Where(item => item.Id == id).SingleOrDefault();
            return await Task.FromResult(item);
        }

        public async Task CreateItemAsync(Item item)
        {
            await Task.Run(() => Items.Add(item));
            //var toReturn = item;
            //return await Task.FromResult(toReturn);
        }

        public async Task UpdateItemAsync(Item item)
        {
            var index = await Task.Run(() => Items.FindIndex(existingItem => existingItem.Id == item.Id));
            Items[index] = item;
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var index = Items.FindIndex(existingItem => existingItem.Id == id);
            await Task.Run(() => Items.RemoveAt(index));
        }
    }
}
 