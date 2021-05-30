using System;

namespace Blazor.Markdown.Shared.Model
{
    public class SettingsModel
    {
        public Guid Id { get; set; }
        public string ConnectionString { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastAdded { get; set; }
    }
}
