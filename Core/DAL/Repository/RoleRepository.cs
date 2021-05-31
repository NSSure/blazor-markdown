using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using SureInjector.Attributes;
using SureInjector.Enums;

namespace Blazor.Markdown.Core.DAL.Repository
{
    [Injection(RequestInjectionState.Transient)]
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(MongoDBContext context) : base(context)
        {

        }
    }
}
