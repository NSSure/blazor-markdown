using System;

namespace Blazor.Markdown
{
    public partial class Constants
    {
        public partial class Permissions
        {
            public partial class Actions
            {
                public partial class Settings
                {
                    public const string Add = "Settings.Add";
                    public static Guid AddId = Guid.Parse("84FCF77E-6FE0-495D-B4FD-74A169C19855");

                    public const string Update = "Settings.Update";
                    public static Guid UpdateId = Guid.Parse("AFD3A7CC-BE93-4A71-9ACB-9013BBE4F0EA");

                    public const string Delete = "Settings.Delete";
                    public static Guid DeleteId = Guid.Parse("1B2A325C-2CD7-4D4F-B9ED-484D48FF3CA6");

                    public const string List = "Settings.List";
                    public static Guid ListId = Guid.Parse("CB9CDB4D-AA5E-49C7-8297-AF5B77DEC642");
                }
            }
        }
    }
}
