using CatalogAPI.Contracts;
using CatalogAPI.Entities;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatalogAPI.Repositories
{
    public class MongoRepository<T> : IRepository<T> where T : IEntity
    {
        private readonly IMongoCollection<T> dbCollection;

        private readonly FilterDefinitionBuilder<T> filterBuilder = Builders<T>.Filter;

        public MongoRepository(IMongoDatabase database, string collectionName)
        {
            dbCollection = database.GetCollection<T>(collectionName);
        }

        public async Task<IReadOnlyCollection<T>> GetAllItemsAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync();
        }

        public async Task<T> GetItemAsync(Guid id)
        {
            FilterDefinition<T> filter = filterBuilder.Eq(item => item.Id, id);
            return await dbCollection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task CreateItemAsync(T item)
        {
            if(item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            await dbCollection.InsertOneAsync(item);
        }

        public async Task UpdateItemAsync(T item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var filter = filterBuilder.Eq(existingItem => existingItem.Id, item.Id);
            await dbCollection.ReplaceOneAsync(filter, item);
        }

        public async Task DeleteItemAsync(Guid id)
        {
            var filter = filterBuilder.Eq(item => item.Id, id);
            await dbCollection.DeleteOneAsync(filter);
        }
    }
}
