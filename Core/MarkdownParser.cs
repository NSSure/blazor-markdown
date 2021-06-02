using System;
using System.Collections.Generic;

namespace Blazor.Markdown.Core
{
    public enum TargetElement
    {
        Header = 0,
        Paragraph = 1,
        Bold = 2,
        Italic = 3,
        BoldAndItalic = 4,
        BlockQuote = 5
    }

    public static class MarkdownParser
    {
        public static Dictionary<TargetElement, List<string>> ElementSyntaxKeyMap = new Dictionary<TargetElement, List<string>>()
        {
            { TargetElement.Header, new List<string>() { "#", "=", "-" } },
            { TargetElement.Paragraph, new List<string>() { "\n" } },
            { TargetElement.Bold, new List<string>() { "**", "__" } },
            { TargetElement.Italic, new List<string>() { "*", "_" } },
            { TargetElement.BoldAndItalic, new List<string>() { "***", "___" } },
            { TargetElement.BlockQuote, new List<string>() { ">" } }
        };

        public static string Compile(string source)
        {
            if (!string.IsNullOrEmpty(source))
            {
                foreach (string line in source.Split(Environment.NewLine))
                {

                }
            }

            return string.Empty;
        }
    }
}
