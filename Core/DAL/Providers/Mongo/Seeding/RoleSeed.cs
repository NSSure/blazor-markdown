using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class RoleSeed : RegisterSeed<Role>
    {
        public override void Configure(MarkdownDBContext context)
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
                ActionKeys = new string[4]
                { 
                    Constants.Permissions.Actions.Settings.Add,
                    Constants.Permissions.Actions.Settings.Update,
                    Constants.Permissions.Actions.Settings.Delete,
                    Constants.Permissions.Actions.Settings.List
                },
                DateAdded = DateTime.UtcNow
            });
        }
    }
}
