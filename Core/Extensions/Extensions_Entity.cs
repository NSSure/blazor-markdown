using Blazor.Markdown.Core.DAL.Entity;
using Blazor.Markdown.Shared.Model;

namespace Blazor.Markdown
{
    public static class Extensions_Entity
    {
        public static RoleModel In(this Role role)
        {
            return new RoleModel()
            {
                Id = role.Id,
                Name = role.Name,
                Key = role.Key,
                DateAdded = role.DateAdded
            };
        }

        public static ActionModel In(this Action action)
        {
            return new ActionModel()
            {
                Id = action.Id,
                Name = action.Name,
                Key = action.Key,
                DateAdded = action.DateAdded
            };
        }
    }
}
