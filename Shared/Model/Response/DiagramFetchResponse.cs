using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Shared.Model.Returns
{
    public class DiagramFetchResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Component> Components { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
