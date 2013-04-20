using Blog.Web.Controllers;
using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web
{
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Runs Markdown to render the source markdown string into HTML
        /// </summary>
        public static MvcHtmlString Markdown<T>(this HtmlHelper<T> html, string source)
        {
            var markdown = html.ViewData[BaseController.VIEWDATA_MARKDOWN] as Markdown;
            if (markdown == null)
            {
                throw new Exception("No Markdown could be found in the ViewData.");
            }
            var strrrr = markdown.Transform(source);
            return new MvcHtmlString(strrrr);
        }
    }
}