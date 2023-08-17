using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;
using Catalog.Common.Settings;
using CatalogAPI.Common.Settings;

namespace Catalog.Common.MongoDB
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));            //Displays Guid type as a string
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));  //Displays datatime as string


            //Add MongoClient 
            services.AddSingleton(ServiceProvider =>
            {
                var configuration = ServiceProvider.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);
                return mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddMongoRepository<T>(this IServiceCollection services, string collectionName)
            where T : IEntity
        {
            services.AddSingleton<IRepository<T>>(ServiceProvider =>
            {
                var database = ServiceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database, collectionName);
            });

            return services;
        }
    }
}



