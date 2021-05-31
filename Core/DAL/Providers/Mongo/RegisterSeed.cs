using Blazor.Markdown.Core.DAL.Mongo;
using System;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public interface IRegisterSeed
    {
        public void Execute(MongoDBContext context);
    }

    public abstract class RegisterSeed<TDocument> : IRegisterSeed
    {
        public RegisterSeed()
        {

        }

        public void Execute(MongoDBContext context)
        {
            this.Configure(context);
        }

        public abstract void Configure(MongoDBContext context);
    }
}
