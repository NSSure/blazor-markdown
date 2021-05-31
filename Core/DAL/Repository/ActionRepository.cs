using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Core.DAL.Mongo;
using SureInjector.Attributes;
using SureInjector.Enums;

namespace Blazor.Markdown.Core.DAL.Repository
{
    [Injection(RequestInjectionState.Transient)]
    public class ActionRepository : BaseRepository<Action>
    {
        public ActionRepository(MongoDBContext context) : base(context)
        {

        }
    }
}
