using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TemplateEngine
{
    public partial class Engine
    {
        private string ParseMenuForeach(string templateData, string pageName)
        {
            var foreachpattern = @"\{\{t foreach (ordered|)page \/(.+)\/\}\}((?:.|\n)+)\{\{t endforeach\}\}";
            return Regex.Replace(templateData, foreachpattern, m => ParseForeachTemplateRegex(m, pageName), RegexOptions.ECMAScript);
        }

        private string ParseForeachTemplateRegex(Match m, string pageName)
        {
            var pages = GetPageNamesMatchRegex(m.Groups[2].Value, m.Groups[1].Value == "ordered");
            var o = pages.Aggregate("", (current, page) => current + ParseForeachTemplateData(m.Groups[3].Value, page, pageName));

            return o;
        }

        /// <summary>
        /// Gets a list of all page names matching an input Regex.
        /// </summary>
        /// <param name="regex">The Regex to match to.</param>
        /// <returns>A list of all matching pages.</returns>
        public List<string> GetPageNamesMatchRegex(string regex, bool isOrdered)
        {
            var pages = isOrdered ? Pages.Where(p => p.Value.ContainsKey("order")).OrderBy(p => GetPageOrder(p.Value)).ToList() : Pages.ToList();
            
            return (from page in pages
                where (page.Key != "default" && Regex.IsMatch(page.Key, regex))
                select page.Key).ToList();
        }

        private int GetPageOrder(Dictionary<string, string> page)
        {
            int i;
            int.TryParse(page["order"], out i);
            return i;
        }

        private string ParseForeachTemplateData(string data, string pageName, string activePage)
        {
            var regex = @"\{\{t (.)\.([a-z]+)(?: +([a-zA-Z0-9_/<>""= ]*))*\}\}";
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
