using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core.DAL.Entity
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Language { get; set; }
        public string IPAddress { get; set; }
        public List<Guid> RoleIds { get; set; }
        public List<Guid> ActionIds { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLastUpdated { get; set; }
    }
}
