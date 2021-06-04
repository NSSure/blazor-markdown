using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Shared.Model
{
    public class DiagramModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
