using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Shared.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class DiagramSeed : RegisterSeed<Diagram>
    {
        public override void Configure(MarkdownDBContext context)
        {
            foreach (int diagramNumber in Enumerable.Range(1, 20))
            {
                Guid _component1Id = Guid.NewGuid();
                Guid _component2Id = Guid.NewGuid();
                Guid _component3Id = Guid.NewGuid();
                Guid _component4Id = Guid.NewGuid();
                Guid _component5Id = Guid.NewGuid();
                Guid _component6Id = Guid.NewGuid();

                context.Diagram.InsertOne(new Diagram()
                {
                    Name = "Lamp Troubleshooting",
                    Tags = new List<string>()
                    {
                        "troubleshooting"
                    },
                    Components = new List<Component>()
                    {
                        new Component()
                        {
                            Id = _component1Id,

                            Position = new Position()
                            {
                                X = 400,
                                Y = 400,
                                Width = 200,
                                Height = 50
                            },
                            Connections = new List<Connection>()
                            {
                                new Connection()
                                {
                                    ComponentId = _component2Id,
                                    SourceCardinal = CardinalDirection.Bottom,
                                    TargetCardinal = CardinalDirection.Top
                                }
                            }
                        },
                        new Component()
                        {
                            Id = _component2Id,
                            Position = new Position()
                            {
                                X = 400,
                                Y = 500,
                                Width = 200,
                                Height = 50
                            },
                            Connections = new List<Connection>()
                            {
                                new Connection()
                                {
                                    ComponentId = _component3Id,
                                    SourceCardinal = CardinalDirection.Right,
                                    TargetCardinal = CardinalDirection.Left
                                },
                                new Connection()
                                {
                                    ComponentId = _component4Id,
                                    SourceCardinal = CardinalDirection.Bottom,
                                    TargetCardinal = CardinalDirection.Top
                                }
                            }
                        },
                        new Component()
                        {
                            Id = _component3Id,
                            Position = new Position()
                            {
                                X = 700,
                                Y = 500,
                                Width = 200,
                                Height = 50
                            }
                        },
                        new Component()
                        {
                            Id = _component4Id,
                            Position = new Position()
                            {
                                X = 400,
                                Y = 600,
                                Width = 200,
                                Height = 50
                            },
                            Connections = new List<Connection>()
                            {
                                new Connection()
                                {
                                    ComponentId = _component5Id,
                                    SourceCardinal = CardinalDirection.Right,
                                    TargetCardinal = CardinalDirection.Left
                                },
                                new Connection()
                                {
                                    ComponentId = _component6Id,
                                    SourceCardinal = CardinalDirection.Bottom,
                                    TargetCardinal = CardinalDirection.Top
                                }
                            }
                        },
                        new Component()
                        {
                            Id = _component5Id,
                            Position = new Position()
                            {
                                X = 700,
                                Y = 600,
                                Width = 200,
                                Height = 50
                            }
                        },
                        new Component()
                        {
                            Id = _component6Id,
                            Position = new Position()
                            {
                                X = 500, // Offset slightly right to test kinked lines.
                                Y = 700,
                                Width = 200,
                                Height = 50
                            }
                        },
                    },
                    DateAdded = DateTime.UtcNow,
                    DateLastUpdated = DateTime.UtcNow
                });
            }
        }
    }
}
