using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using System;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class RoleSeed : RegisterSeed<Role>
    {
        public override void Configure(MongoDBContext context)
        {
            context.Role.InsertOne(new Role()
            {
                Id = Guid.Parse("AE2AB2DC-7CB9-4065-AB31-12613CE08F96"),
                Name = "Administrator",
                Key = "System.Admin",
                DateAdded = DateTime.UtcNow
            });

            context.Role.InsertOne(new Role()
            {
                Id = Guid.Parse("536DDEB8-C050-45D9-94B7-2A365D88EB52"),
                Name = "User",
                Key = "System.User",
                DateAdded = DateTime.UtcNow
            });
        }
    }
}
