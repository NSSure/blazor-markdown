using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Entity
{
    [BsonNoId]
    public class Collection
    {
        public Guid ID { get; set; }
        public Dictionary<string, Guid> Nodes { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
