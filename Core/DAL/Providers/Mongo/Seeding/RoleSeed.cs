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
                Id = Constants.Permissions.Roles.SystemAdminId,
                Key = Constants.Permissions.Roles.SystemAdmin,
                Name = "Administrator",
                DateAdded = DateTime.UtcNow
            });

            context.Role.InsertOne(new Role()
            {
                Id = Constants.Permissions.Roles.SystemUserId,
                Key = Constants.Permissions.Roles.SystemUser,
                Name = "User",
                DateAdded = DateTime.UtcNow
            });
        }
    }
}
