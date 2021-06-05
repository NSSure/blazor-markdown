using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Shared.Model
{
    public class Component
    {
        public Guid Id { get; set; }
        public Position Position { get; set; }
        public Material Material { get; set; }
        public List<Connection> Connections { get; set; } = new List<Connection>();
    }
}
