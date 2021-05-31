using System;

namespace Blazor.Markdown.Core.DAL.Entity
{
    public class Action
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
