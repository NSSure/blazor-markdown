using Blazor.Markdown.Core.Constants;
using Blazor.Markdown.Core.DAL.Entity;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core
{
    public class IndexRegistrations
    {
        public Type Entity { get; set; }
        public List<BsonDocument> Indexes { get; set; }
    }

    public class DBConfiguration
    {
        public string Name { get; set; }
        public List<EntityConfiguration> EntityConfigurations { get; set; } = new List<EntityConfiguration>();
    }

    public class EntityConfiguration
    {
        public string Name { get; set; }
        public Dictionary<string, List<string>> IndexConfigurations { get; set; } = new Dictionary<string, List<string>>();
    }

    public static class MarkdownApp
    {
        public static DBConfiguration DBConfiguration { get; set; }

        public static User CurrentUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "Nicholas Gordon",
            DateAdded = DateTime.UtcNow,
            DateLastUpdated = DateTime.UtcNow
        };

        public static List<string> CurrentActions = new List<string>()
        {
            Permissions.Actions.Settings.Add,
            Permissions.Actions.Settings.List
        };
    }
}
