using GiftGivr.Web.Classes;
using GiftGivr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftGivr.Web.Controllers
{
    public class CommentController : BaseController
    {
        public CommentController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult Add(int giftId)
        {
            var gift = DataContext.Gifts.Single(a => a.GiftId == giftId);
            var viewModel = new AddCommentViewModel
            {
                Gift = gift.Name,
                GiftCreator = DataContext.Accounts.Single(a => a.AccountId == gift.CreatorAccountId).Name,
                Comment = null
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(int giftId, GiftCommentViewModel model)
        {
            var comment = new Data.Comment
            {
                AccountId = UserId.Value,
                CommentText =  model.Comment,
                GiftId = giftId,
                Timestamp = DateTime.UtcNow
            };
            DataContext.Comments.InsertOnSubmit(comment);
            DataContext.SubmitChanges();

            var userId = DataContext.Gifts.Single(a => a.GiftId == giftId).TargetAccountId;
            return RedirectToAction("View", "Gifts", new { userId = userId });
        }
    }
}
