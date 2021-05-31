using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;

namespace Blazor.Markdown.Core.DAL.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MongoDBContext context) : base(context)
        {

        }
    }
}
