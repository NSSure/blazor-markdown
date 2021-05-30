using Blazor.Markdown.Core.DAL.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Mongo.Map
{
    public class CollectionMap : BsonClassMap<Collection>
    {
        public CollectionMap()
        {
            this.MapIdMember(x => x.ID).SetOrder(1).SetIdGenerator(CombGuidGenerator.Instance);

            this.MapMember(x => x.Nodes).SetOrder(2).SetSerializer(new DictionaryInterfaceImplementerSerializer<Dictionary<string, Guid>>(DictionaryRepresentation.Document)).SetIsRequired(true);
            this.MapMember(x => x.DateAdded).SetOrder(3).SetIsRequired(true);
            this.MapMember(x => x.DateLastUpdated).SetOrder(4).SetIsRequired(true);
        }
    }
}
