using Blog.Service;
using MarkdownSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly BlogControllerContext _context;
        protected readonly IBlogService BlogService;
        protected BaseController(BlogControllerContext context)
        {
            _context = context;

            BlogService = context.BlogService;
        }

        private Markdown GetMarkdown()
        {
            return new Markdown();
        }

        #region ViewData Cramming

        public const string VIEWDATA_MARKDOWN = "VIEWDATA_MARKDOWN";

        protected virtual void CramViewData()
        {
            ViewData[VIEWDATA_MARKDOWN] = GetMarkdown();
        }

        #region Decorators

        protected override ViewResult View(IView view, object model)
        {
            CramViewData();
            return base.View(view, model);
        }

        protected override ViewResult View(string viewName, string masterName, object model)
        {
            CramViewData();
            return base.View(viewName, masterName, model);
        }

        protected override PartialViewResult PartialView(string viewName, object model)
        {
            CramViewData();
            return base.PartialView(viewName, model);
        }

        #endregion

        #endregion
    }
}
