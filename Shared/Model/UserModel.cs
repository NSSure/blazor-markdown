using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Shared.Model
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Guid> ActionIds { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
