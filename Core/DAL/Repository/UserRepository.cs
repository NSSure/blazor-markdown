using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using SureInjector.Attributes;
using SureInjector.Enums;

namespace Blazor.Markdown.Core.DAL.Repository
{
    [Injection(RequestInjectionState.Transient)]
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(MongoDBContext context) : base(context)
        {

        }
    }
}
