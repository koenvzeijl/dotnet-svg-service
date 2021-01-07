using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotnetSvgService.Services
{
    public class Icons
    {
        private readonly IWebHostEnvironment _environment;
        private readonly HashSet<string> _usedIcons = new HashSet<string>();


        public Icons(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        // Place all your icons here
        public HtmlString Download => SvgIcon("download");

        public HtmlString WriteSymbols()
        {
            var builder = new StringBuilder();
            builder.AppendLine(@"<svg xmlns=""http://www.w3.org/2000/svg"" style=""display: none"">");

            foreach (var icon in _usedIcons)
            {
                var iconPath = Path.Join(_environment.WebRootPath, "icons", icon);
                if (!File.Exists(iconPath))
                    continue;

                builder.AppendLine(File.ReadAllText(iconPath));
            }

            builder.Append("</svg>");

            return new HtmlString(builder.ToString());
        }

        public HtmlString SvgIcon(string name)
        {
            _usedIcons.Add(name + ".svg");

            return new HtmlString($@"<svg class=""icon icon-{name}""><use xlink:href=""#icon-{name}""></use></svg>");
        }
    }
}
