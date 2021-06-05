using System;

namespace Blazor.Markdown.Shared.Model
{
    public enum CardinalDirection
    {
        Left = 0,
        Top = 1,
        Right = 2,
        Bottom = 3
    }

    public class Connection
    {
        public Guid ComponentId { get; set; }
        public CardinalDirection SourceCardinal { get; set; }
        public CardinalDirection TargetCardinal { get; set; }
    }
}
