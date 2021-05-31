using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using MongoDB.Driver;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MongoDBOptions
    {
        public bool EnsureCreated { get; set; }

        public void AddSeeding()
        {

        }
    }

    public static class MongoDBDependencyInjectionExtensions
    {
        public static MongoDBContextBuilder AddMongoDB(this IServiceCollection services, Type contextType)
        {
            if (contextType != typeof(MongoDBContext))
            {
                throw new Exception("The context type must either derive from or be the MongoDBContext class.");
            }

            // Register DB context with DI container.
            services.AddSingleton(contextType);

            return new MongoDBContextBuilder();
        }

        public static MongoDBContextBuilder AddMongoDB(this IServiceCollection services, Type contextType, Action<MongoDBOptions> configureOptions)
        {
            if (contextType != typeof(MongoDBContext))
            {
                throw new Exception("The context type must either derive from or be the MongoDBContext class.");
            }

            // Register DB context with DI container.
            services.AddSingleton(contextType);

            // Configure options.
            MongoDBOptions _mongoDBOptions = new MongoDBOptions();
            configureOptions?.Invoke(_mongoDBOptions);

            MongoDBContextBuilder _builder = new MongoDBContextBuilder();

            if (_mongoDBOptions.EnsureCreated)
            {
                MongoDBContext _context = (MongoDBContext)Activator.CreateInstance(contextType);

                IMongoDatabase _existingDatabase = _context.Client.GetDatabase(typeof(MongoDBContext).Name);

                // Indexes are idempotent so should this just run every time the application starts?
                _builder.ExecuteMappingRegistrations();
                _builder.ExecuteIndexRegistrations();
            }

            return _builder;
        }

        public static MongoDBContextBuilder AddIndexes(this MongoDBContextBuilder builder)
        {
            throw new NotImplementedException();
        }

        public static MongoDBContextBuilder AddMappings(this MongoDBContextBuilder builder)
        {
            throw new NotImplementedException();
        }
    }
}
