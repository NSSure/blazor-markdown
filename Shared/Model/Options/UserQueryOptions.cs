namespace Blazor.Markdown.Shared.Model.Options
{
    /// <summary>
    /// The object passed the user API when query users.
    /// </summary>
    public class UserQueryOptions
    {
        public string Role { get; set; }
        public string Action { get; set; }
    }
}
