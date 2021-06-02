using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public class MongoDBOptions
    {
        public bool EnsureCreated { get; set; }
        public bool DropDatabaseOnLoad { get; set; }
    }

    public static class MongoDBDependencyInjectionExtensions
    {
        public static MongoDBContextBuilder<TContext> AddMongoDB<TContext>(this IServiceCollection services, Type contextType) where TContext : MongoDBContext
        {
            if (contextType != typeof(MarkdownDBContext))
            {
                throw new Exception("The context type must either derive from or be the MongoDBContext class.");
            }

            // Register DB context with DI container.
            services.AddSingleton(contextType);

            return new MongoDBContextBuilder<TContext>();
        }

        public static MongoDBContextBuilder<TContext> AddMongoDB<TContext>(this IServiceCollection services, Action<MongoDBOptions> configureOptions) where TContext : MongoDBContext
        {
            if (typeof(TContext) != typeof(MarkdownDBContext))
            {
                throw new Exception("The context type must either derive from or be the MongoDBContext class.");
            }

            // Register DB context with DI container.
            services.AddSingleton(typeof(TContext));

            // Configure user defined options.
            MongoDBOptions _mongoDBOptions = new MongoDBOptions();
            configureOptions?.Invoke(_mongoDBOptions);

            // Construct the context builder and run the initialization process.
            MongoDBContextBuilder<TContext>_builder = new MongoDBContextBuilder<TContext>(_mongoDBOptions);

            _builder.Run();

            return _builder;
        }
    }
}
