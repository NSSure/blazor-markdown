using Blazor.Markdown.Core.Attributes;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;

namespace Blazor.Markdown.Core.Mediator.Request
{
    [AuthorizeAction(Constants.Permissions.Actions.Settings.Add)]
    public class DiagramFetchRequest : IRequest<DiagramFetchResponse>
    {
        public DiagramFetchRequest()
        {

        }
    }
}
