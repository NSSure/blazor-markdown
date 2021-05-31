using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MongoDBOptions
    {
        public bool EnsureCreated { get; set; }
    }

    public static class MongoDBDependencyInjectionExtensions
    {
        public static MongoDBContextBuilder<TContext> AddMongoDB<TContext>(this IServiceCollection services, Type contextType)
        {
            if (contextType != typeof(MongoDBContext))
            {
                throw new Exception("The context type must either derive from or be the MongoDBContext class.");
            }

            // Register DB context with DI container.
            services.AddSingleton(contextType);

            return new MongoDBContextBuilder<TContext>();
        }

        public static MongoDBContextBuilder<TContext> AddMongoDB<TContext>(this IServiceCollection services, Type contextType, Action<MongoDBOptions> configureOptions)
        {
            if (contextType != typeof(MongoDBContext))
            {
                throw new Exception("The context type must either derive from or be the MongoDBContext class.");
            }

            // Register DB context with DI container.
            services.AddSingleton(contextType);

            // Configure user defined options.
            MongoDBOptions _mongoDBOptions = new MongoDBOptions();
            configureOptions?.Invoke(_mongoDBOptions);

            // Inject to builder through DI container?
            MongoDBContext _context = (MongoDBContext)Activator.CreateInstance(contextType);

            // Construct the context builder and run the initialization process.
            MongoDBContextBuilder<TContext>_builder = new MongoDBContextBuilder<TContext>(_mongoDBOptions);
            _builder.Run();

            return _builder;
        }

        //public static MongoDBContextBuilder AddIndexes(this MongoDBContextBuilder builder)
        //{
        //    throw new NotImplementedException();
        //}

        //public static MongoDBContextBuilder AddMappings(this MongoDBContextBuilder builder)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
