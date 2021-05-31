using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class UserSeed : RegisterSeed<User>
    {
        public override void Configure(MongoDBContext context)
        {
            context.User.InsertOne(new User()
            {
                Name = "System Admin",
                ActionIds = new List<Guid>()
                {
                    Constants.Permissions.Actions.Settings.AddId,
                    Constants.Permissions.Actions.Settings.UpdateId,
                    Constants.Permissions.Actions.Settings.DeleteId,
                    Constants.Permissions.Actions.Settings.ListId
                },
                DateAdded = DateTime.UtcNow,
                DateLastUpdated = DateTime.UtcNow
            });

            context.User.InsertOne(new User()
            {
                Name = "John Doe",
                ActionIds = new List<Guid>(),
                DateAdded = DateTime.UtcNow,
                DateLastUpdated = DateTime.UtcNow
            });
        }
    }
}
