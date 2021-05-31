﻿using Blazor.Markdown.Core.Attributes;
using Blazor.Markdown.Core.Constants;
using Blazor.Markdown.Shared.Model.Options;
using Blazor.Markdown.Shared.Model.Response;
using MediatR;

namespace Blazor.Markdown.Core.Mediator.Request
{
    [AuthorizeAction(Permissions.Actions.Settings.Add)]
    public class SettingsCreationRequest : IRequest<SettingsCreationResponse>
    {
        public SettingsCreationRequest(SettingsCreationOptions options)
        {
            this.Options = options;
        }

        public SettingsCreationOptions Options { get; set; }
    }
}
