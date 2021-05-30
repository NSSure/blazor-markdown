using Blazor.Markdown.Shared.Enum;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Blazor.Markdown.Core.DAL.Entity
{
    [BsonNoId]
    public class Note
    {
        public Guid ID { get; set; }
        public NoteType Type { get; set; }
        public string Text { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
