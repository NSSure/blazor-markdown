using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Blazor.Markdown.Core.DAL.Mongo.Map
{
    public class UserMap : RegisterMap<User>
    {
        public UserMap()
        {

        }

        public override void Configure(BsonClassMap<User> builder)
        {
            builder.MapIdMember(x => x.Id).SetOrder(1).SetIdGenerator(CombGuidGenerator.Instance);

            builder.MapMember(x => x.Name).SetOrder(2).SetIsRequired(true);
            builder.MapMember(x => x.RoleIds).SetOrder(3).SetIsRequired(true);
            builder.MapMember(x => x.ActionIds).SetOrder(4).SetIsRequired(true);
            builder.MapMember(x => x.DateAdded).SetOrder(5).SetIsRequired(true);
            builder.MapMember(x => x.DateLastUpdated).SetOrder(6).SetIsRequired(true);
        }
    }
}
