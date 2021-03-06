using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Entity
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public string[] ActionKeys { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
