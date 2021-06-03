using Blazor.Markdown.Shared.Model;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Entity
{
    public class Diagram
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Component> Components { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
