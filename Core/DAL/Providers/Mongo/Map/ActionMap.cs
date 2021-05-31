using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Blazor.Markdown.Core.DAL.Mongo.Map
{
    public class ActionMap : RegisterMap<Action>
    {
        public ActionMap()
        {

        }

        public override void Configure(BsonClassMap<Action> builder)
        {
            builder.MapIdMember(x => x.Id).SetOrder(1).SetIdGenerator(CombGuidGenerator.Instance);

            builder.MapMember(x => x.Name).SetOrder(2).SetIsRequired(true);
            builder.MapMember(x => x.Key).SetOrder(3).SetIsRequired(true);
            builder.MapMember(x => x.DateAdded).SetOrder(3).SetIsRequired(true);
        }
    }
}
