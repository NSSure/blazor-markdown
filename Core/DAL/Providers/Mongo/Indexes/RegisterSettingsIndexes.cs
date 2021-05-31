using Blazor.Markdown.Core.DAL.Entity;

namespace Blazor.Markdown.Core.DAL.Providers.Mongo.Indexes
{
    public class RegisterSettingsIndexes : RegisterIndexes<Settings>
    {
        public RegisterSettingsIndexes()
        {

        }

        public override void Configure(IndexBuilder<Settings> builder)
        {
            builder.Ascending(x => x.ConnectionString);
        }
    }
}
