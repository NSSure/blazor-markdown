using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class UserSeed : RegisterSeed<User>
    {
        public override string SeedFile => "USERS_MOCK_DATA.json";

        public override void Configure(MarkdownDBContext context)
        {
            var _mockUsersJson = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), this.SeedFile));
            var _mockUsers = JsonConvert.DeserializeObject<List<User>>(_mockUsersJson);

            context.User.InsertManyAsync(_mockUsers);

            context.User.InsertOne(new User()
            {
                Name = "System Admin",
                RoleIds = new List<Guid>()
                {
                    Constants.Permissions.Roles.SystemAdminId
                },
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
                RoleIds = new List<Guid>()
                {
                    Constants.Permissions.Roles.SystemUserId
                },
                ActionIds = new List<Guid>(),
                DateAdded = DateTime.UtcNow,
                DateLastUpdated = DateTime.UtcNow
            });
        }
    }
}
