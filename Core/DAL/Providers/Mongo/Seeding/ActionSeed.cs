using Blazor.Markdown.Core.DAL.Mongo;
using System;
using System.Collections.Generic;

using Action = Blazor.Markdown.Core.DAL.Entity.Action;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class ActionSeed : RegisterSeed<Action>
    {
        public override void Configure(MarkdownDBContext context)
        {
            context.Action.InsertOne(new Action()
            {
                Id = Constants.Permissions.Actions.Settings.AddId,
                Name = "Add Settings",
                Key = "Settings.Add",
                RoleIds = new List<Guid>()
                {
                    Constants.Permissions.Roles.SystemAdminId,
                    Constants.Permissions.Roles.SystemUserId
                },
                DateAdded = DateTime.UtcNow
            });

            context.Action.InsertOne(new Action()
            {
                Id = Constants.Permissions.Actions.Settings.UpdateId,
                Name = "Update Settings",
                Key = "Settings.Update",
                RoleIds = new List<Guid>()
                {
                    Constants.Permissions.Roles.SystemAdminId,
                    Constants.Permissions.Roles.SystemUserId
                },
                DateAdded = DateTime.UtcNow
            });

            context.Action.InsertOne(new Action()
            {
                Id = Constants.Permissions.Actions.Settings.DeleteId,
                Name = "Delete Settings",
                Key = "Settings.Delete",
                RoleIds = new List<Guid>()
                {
                    Constants.Permissions.Roles.SystemAdminId,
                    Constants.Permissions.Roles.SystemUserId
                },
                DateAdded = DateTime.UtcNow
            });

            context.Action.InsertOne(new Action()
            {
                Id = Constants.Permissions.Actions.Settings.ListId,
                Name = "List Settings",
                Key = "Settings.List",
                RoleIds = new List<Guid>()
                {
                    Constants.Permissions.Roles.SystemAdminId,
                    Constants.Permissions.Roles.SystemUserId
                },
                DateAdded = DateTime.UtcNow
            });
        }
    }
}
