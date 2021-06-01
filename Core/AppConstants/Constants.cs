using System;

namespace Blazor.Markdown
{
    public partial class Constants
    {
        public partial class Permissions
        {
            public partial class Roles
            {
                public const string SystemAdmin = "System.Admin";
                public static Guid SystemAdminId = Guid.Parse("0A09D6A7-F34A-477C-AF27-170BAD7B108D");

                public const string SystemUser = "System.User";
                public static Guid SystemUserId = Guid.Parse("52B306A5-60EE-402C-9E6B-FAF6D080E641");
            }
        }
    }
}
