using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Shared.Enum;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Blazor.Markdown.Core.DAL.Mongo.Map
{
    public class NoteMap : BsonClassMap<Note>
    {
        public NoteMap()
        {
            this.MapIdMember(x => x.ID).SetOrder(1).SetIdGenerator(CombGuidGenerator.Instance);

            this.MapMember(x => x.Text).SetOrder(2).SetIsRequired(true);
            this.MapMember(x => x.Type).SetOrder(3).SetDefaultValue(NoteType.Public).SetSerializer(new EnumSerializer<NoteType>(BsonType.String)).SetIsRequired(true);
            this.MapMember(x => x.DateAdded).SetOrder(3).SetIsRequired(true);
        }
    }
}
