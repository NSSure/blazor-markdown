using System;

namespace Blazor.Markdown.Shared.Model
{
    public class ActionModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public DateTime DateAdded { get; set; }
    }
}
