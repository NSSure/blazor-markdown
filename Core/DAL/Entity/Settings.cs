using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Blazor.Markdown.Core.DAL.Entity
{
    [BsonNoId]
    public class Settings
    {
        public Settings()
        {
            this.DateAdded = DateTime.UtcNow;
            this.DateLastUpdated = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public string ConnectionString { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
