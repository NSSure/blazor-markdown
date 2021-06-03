using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Shared.Model
{
    public class RoleModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Key { get; set; }
        public List<ActionModel> Actions { get; set; } = new List<ActionModel>();
        public DateTime DateAdded { get; set; }
    }
}
