using Blazor.Markdown.Core.Attributes;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;

namespace Blazor.Markdown.Core.Mediator.Request
{
    [AuthorizeAction(Constants.Permissions.Actions.Settings.Add)]
    public class UserActionRemovalRequest : IRequest<UserActionRemovalResponse>
    {
        public UserActionRemovalRequest(UserActionRemovalOptions options)
        {
            this.Options = options;
        }

        public UserActionRemovalOptions Options { get; set; }
    }
}
