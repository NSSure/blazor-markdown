using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Providers.Mongo;
using Blazor.Markdown.Shared.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Blazor.Markdown.Core.DAL.Mongo.Map
{
    public class NoteMap : RegisterMap<Note>
    {
        public NoteMap()
        {

        }

        public override void Configure(BsonClassMap<Note> builder)
        {
            builder.MapIdMember(x => x.ID).SetOrder(1).SetIdGenerator(CombGuidGenerator.Instance);

            builder.MapMember(x => x.Text).SetOrder(2).SetIsRequired(true);
            builder.MapMember(x => x.Type).SetOrder(3).SetDefaultValue(NoteType.Public).SetSerializer(new EnumSerializer<NoteType>(BsonType.String)).SetIsRequired(true);
            builder.MapMember(x => x.DateAdded).SetOrder(3).SetIsRequired(true);
        }
    }
}
