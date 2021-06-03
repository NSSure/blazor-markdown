using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using Blazor.Markdown.Shared.Model;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Seeding
{
    public class DiagramSeed : RegisterSeed<Diagram>
    {
        public override void Configure(MarkdownDBContext context)
        {
            context.Diagram.InsertOne(new Diagram()
            {
                Name = "Sample Diagram",
                Components = new List<Component>()
                {
                    new Component()
                    {
                        Position = new Position()
                        {
                            X = 100,
                            Y = 100,
                            Width = 100,
                            Height = 100
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
