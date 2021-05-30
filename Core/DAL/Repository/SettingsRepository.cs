using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;

namespace Blazor.Markdown.Core.DAL.Repository
{
    public class SettingsRepository : BaseRepository<Settings>
    {
        public SettingsRepository(MongoDBContext context) : base(context)
        {

        }
    }
}
