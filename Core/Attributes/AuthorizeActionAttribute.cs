using System;

namespace Blazor.Markdown.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class AuthorizeActionAttribute : Attribute
    {
        public string Action { get; set; }

        public AuthorizeActionAttribute(string action)
        {
            this.Action = action;
        }
    }
}
