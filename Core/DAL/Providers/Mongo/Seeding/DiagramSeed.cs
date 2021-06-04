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
                        Position = new Position()
                        {
                            X = 100,
                            Y = 100,
                            Width = 100,
                            Height = 200
                        },
                        Material = new Material()
                        {
                            Color = "#ffffff",
                            BackgroundColor = "#1ED760"
                        }
                    },
                    new Component()
                    {
                        Position = new Position()
                        {
                            X = 300,
                            Y = 300,
                            Width = 100,
                            Height = 200
                        },
                        Material = new Material()
                        {
                            Color = "#ffffff",
                            BackgroundColor = "#1ED760"
                        }
                    }
                },
                    DateAdded = DateTime.UtcNow
                });
            }
        }
    }
}
