using Blazor.Markdown.Core.DAL.Mongo;
using System;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public interface IRegisterSeed
    {
        public void Execute(MarkdownDBContext context);
    }

    public abstract class RegisterSeed<TDocument> : IRegisterSeed
    {
        public RegisterSeed()
        {

        }

        public void Execute(MarkdownDBContext context)
        {
            this.Configure(context);
        }

        public abstract void Configure(MarkdownDBContext context);
    }
}
