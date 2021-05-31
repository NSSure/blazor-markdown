using System;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public class Seed
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
