using Action = Blazor.Markdown.Core.DAL.Entity.Action;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Indexes
{
    public class RegisterActionIndexes : RegisterIndexes<Action>
    {
        public RegisterActionIndexes()
        {

        }

        public override void Configure(IndexBuilder<Action> builder)
        {
            builder.Ascending(x => x.Key);
            builder.Descending(x => x.Key);
            builder.Descending(x => x.DateAdded);
        }
    }
}
