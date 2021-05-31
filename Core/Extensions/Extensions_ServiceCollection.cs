using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ConfigureMongoDBSeedingOptions : IConfigureNamedOptions<Action>
    {
        private readonly MongoDBContext _context;

        public ConfigureMongoDBSeedingOptions(MongoDBContext context)
        {
            // ConfigureMongoDBSeedingOptions constructed from DI, so we can inject anything here
            this._context = context;
        }

        public void Configure(string name, Action options)
        {

        }

        public void Configure(Action options)
        {

        }
    }
}
