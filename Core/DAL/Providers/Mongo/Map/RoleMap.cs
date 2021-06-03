using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using System;

namespace Blazor.Markdown.Core.DAL.Mongo.Map
{
    public class RoleMap : RegisterMap<Role>
    {
        public RoleMap()
        {

        }

        public override void Configure(BsonClassMap<Role> builder)
        {
            builder.MapIdMember(x => x.Id).SetOrder(1).SetIdGenerator(CombGuidGenerator.Instance);

            builder.MapMember(x => x.Name).SetOrder(2).SetIsRequired(true);
            builder.MapMember(x => x.Key).SetOrder(3).SetIsRequired(true);
            builder.MapMember(x => x.ActionKeys).SetOrder(4).SetIsRequired(true);
            builder.MapMember(x => x.DateAdded).SetOrder(5).SetIsRequired(true);
        }
    }
}
