using GiftGivr.Web.Classes;
using GiftGivr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftGivr.Web.Controllers
{
    public class QuestionController : BaseController
    {
        public QuestionController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult Ask(int giftId)
        {
            var gift = DataContext.Gifts.Single(a => a.GiftId == giftId);
            var viewModel = new AskQuestionViewModel
            {
                Gift = gift.Name,
                GiftCreator = DataContext.Accounts.Single(a => a.AccountId == gift.CreatorAccountId).Name,
                Question = null
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Ask(int giftId, AskQuestionViewModel model)
        {
            var question = new Data.GiftQuestion
            {
                AccountId = UserId.Value,
                GiftId = giftId,
                QuestionText = model.Question,
                Timestamp = DateTime.UtcNow
            };
            DataContext.GiftQuestions.InsertOnSubmit(question);
            DataContext.SubmitChanges();

            var gift = DataContext.Gifts.Single(a => a.GiftId == giftId);
            return RedirectToAction("View", "Gifts", new { userId = gift.TargetAccountId });
        }

        [HttpGet]
        public ActionResult Answer(int questionId)
        {
            var question = DataContext.GiftQuestions.Single(a => a.GiftQuestionId == questionId);
            var viewModel = new AnswerQuestionViewModel
            {
                Answer = null,
                Gift = question.Gift.Name,
                GiftCreator = DataContext.Accounts.Single(a => a.AccountId == question.Gift.CreatorAccountId).Name,
                QuestionAsker = DataContext.Accounts.Single(a => a.AccountId == question.AccountId).Name,
                Question = question.QuestionText
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Answer(int questionId, GiftAnswerViewModel model)
        {
            var answer = new Data.GiftAnswer
            {
                GiftQuestionId = questionId,
                AccountId = UserId.Value,
                QuestionAnswer = model.Answer,
                Timestamp = DateTime.UtcNow
            };
            DataContext.GiftAnswers.InsertOnSubmit(answer);
            DataContext.SubmitChanges();

            var accountId = (from q in DataContext.GiftQuestions
                             where q.GiftQuestionId == questionId
                             select q.Gift.TargetAccountId).FirstOrDefault();
            return RedirectToAction("View", "Gifts", new { userId = accountId });
        }
    }
}
