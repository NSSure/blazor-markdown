using Blazor.Markdown.Core.Constants;
using Blazor.Markdown.Core.DAL.Entity;
using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core
{
    public static class MarkdownApp
    {
        public static User CurrentUser = new User()
        {
            Id = Guid.NewGuid(),
            Name = "Nicholas Gordon",
            DateAdded = DateTime.UtcNow,
            DateLastUpdated = DateTime.UtcNow
        };

        public static List<string> CurrentActions = new List<string>()
        {
            Permissions.Actions.Settings.Add,
            Permissions.Actions.Settings.List
        };
    }
}
