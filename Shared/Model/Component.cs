using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Shared.Model
{
    public class Component
    {
        public Guid Id { get; set; }
        public Position Position { get; set; }
        public string BackgroundColor { get; set; } = "#DAE8FC";
        public string StrokeColor { get; set; } = "#6C8EBF";
        public List<Connection> Connections { get; set; } = new List<Connection>();
    }
}
