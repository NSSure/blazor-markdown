using MongoDB.Bson.Serialization;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo
{
    public interface IRegisterMap
    {
        public void Execute();
    }

    public abstract class RegisterMap<TDocument> : IRegisterMap
    {
        public RegisterMap()
        {

        }

        public void Execute()
        {
            BsonClassMap<TDocument> _builder = new BsonClassMap<TDocument>();
            this.Configure(_builder);
            BsonClassMap.RegisterClassMap(_builder);
        }

        public abstract void Configure(BsonClassMap<TDocument> builder);
    }
}
