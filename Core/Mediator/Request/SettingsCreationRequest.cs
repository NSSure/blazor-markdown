using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Returns;
using MediatR;

namespace Blazor.Markdown.Core.Mediator.Request
{
    public class SettingsCreationRequest : IRequest<SettingsCreationReturn>
    {
        public SettingsCreationRequest(SettingsCreationOptions options)
        {
            this.Options = options;
        }

        public SettingsCreationOptions Options { get; set; }
    }
}
