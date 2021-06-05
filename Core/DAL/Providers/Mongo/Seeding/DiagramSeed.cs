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

                context.Diagram.InsertOne(new Diagram()
                {
                    Name = $"Diagram {diagramNumber}",
                    Tags = new List<string>()
                    {
                        "prototype", "internal", "documentation"
                    },
                    Components = new List<Component>()
                    {
                        new Component()
                        {
                            Id = _component1Id,
                            Position = new Position()
                            {
                                X = 100,
                                Y = 100,
                                Width = 200,
                                Height = 100
                            },
                            Material = new Material()
                            {
                                Color = "#ffffff",
                                BackgroundColor = "#007ACC"
                            },
                            Connections = new List<Connection>()
                            {
                                new Connection()
                                {
                                    ComponentId = _component2Id,
                                    SourceCardinal = CardinalDirection.Right,
                                    TargetCardinal = CardinalDirection.Top
                                },
                                new Connection()
                                {
                                    ComponentId = _component3Id,
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
                                Y = 300,
                                Width = 200,
                                Height = 100
                            },
                            Material = new Material()
                            {
                                Color = "#ffffff",
                                BackgroundColor = "#007ACC"
                            }
                        },
                        new Component()
                        {
                            Id = _component3Id,
                            Position = new Position()
                            {
                                X = 100,
                                Y = 400,
                                Width = 200,
                                Height = 100
                            },
                            Material = new Material()
                            {
                                Color = "#ffffff",
                                BackgroundColor = "#007ACC"
                            }
                        }
                    },
                    DateAdded = DateTime.UtcNow
                });
            }
        }
    }
}
