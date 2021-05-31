
namespace Blazor.Markdown.Core.Utility
{
    public static class PermissionsUtility
    {
        /// <summary>
        /// Checks if the current user has the give action.
        /// </summary>
        /// <returns></returns>
        public static bool HasAction(string action)
        {
            if (MarkdownApp.CurrentActions.Contains(action))
            {
                return true;
            }

            return false;
        }
    }
}
