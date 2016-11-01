using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace templategen
{
    internal partial class TemplateEngine
    {
        private string ParseMenuForeach(string templateData, string pageName)
        {
            var foreachpattern = @"\{\{t foreach page \/(.+)\/\}\}((?:.|\n)+)\{\{t endforeach\}\}";
            return Regex.Replace(templateData, foreachpattern, m => ParseForeachTemplateRegex(m, pageName), RegexOptions.ECMAScript);
        }

        private List<string> GetPageNamesMatchRegex(string regex)
        {
            return (from page in _pages where (page.Key != "default" && Regex.IsMatch(page.Key, regex)) select page.Key).ToList();
        }

        private string ParseForeachTemplateRegex(Match m, string pageName)
        {
            var pages = GetPageNamesMatchRegex(m.Groups[1].Value);
            var o = pages.Aggregate("", (current, page) => current + ParseForeachTemplateData(m.Groups[2].Value, page, pageName));

            return o;
        }

        private string ParseForeachTemplateData(string data, string pageName, string activePage)
        {
            var regex = @"\{\{t (.)\.([a-z]+)(?: )*([a-zA-Z0-9_]+)*(?:\w)*\}\}";
            return Regex.Replace(data, regex, m =>
            {
                if (m.Groups[1].Value == "p") // {{t p.
                {
                    if (m.Groups[2].Value == "isactivepage" && m.Groups.Count > 3) // {{t p.isactivepage *}} - Return * if currently the active page
                        return (pageName == activePage) ? m.Groups[3].Value : String.Empty;

                    return GetPropertyValue(pageName, m.Groups[2].Value); // {{t p.title}} - Page title or any other property
                }

                if (m.Groups[1].Value == "a" && m.Groups[2].Value == "p") // {{t a.p}} - Pagename
                    return FormatOutputFilename(pageName);

                return String.Empty;
            });
        }
    }
}
