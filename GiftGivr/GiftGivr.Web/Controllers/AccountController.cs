using GiftGivr.Web.Classes;
using GiftGivr.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace GiftGivr.Web.Controllers
{
    public class AccountController : BaseController
    {
        public AccountController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult Gifts(int userId)
        {
            var primitiveResults = (from g in DataContext.Gifts
                          where g.TargetAccountId == UserId
                            && (UserId != userId || UserId == g.CreatorAccountId)
                          select new
                          {
                              GiftId = g.GiftId,
                              g.ClaimedByAccountId,
                              g.CreatorAccountId,
                              g.TargetAccountId,
                              Name = g.Name,
                              Description = g.Description,
                              Url = g.PurchaseUrl,
                              Questions = g.GiftQuestions.OrderBy(a => a.Timestamp).Select(a => new GiftQuestionViewModel
                              {
                                  Question = a.QuestionText,
                                  Timestamp = a.Timestamp,
                                  User = a.Account.Name,
                                  Answers = a.GiftAnswers.OrderBy(b => b.Timestamp).Select(b => new GiftAnswerViewModel
                                  {
                                      Answer = b.QuestionAnswer,
                                      Timestamp = b.Timestamp,
                                      User = b.Account.Name
                                  }).ToList()
                              }).ToList(),
                              Comments = g.Comments.OrderBy(a => a.Timestamp).Select(a => new GiftCommentViewModel
                              {
                                  Comment = a.CommentText,
                                  Timestamp = a.Timestamp,
                                  User = a.Account.Name
                              }).ToList()
                          }).ToList();

            var allUserIds = primitiveResults.Where(a => a.ClaimedByAccountId.HasValue).Select(a => a.ClaimedByAccountId.Value)
                            .Union(primitiveResults.Select(a => a.CreatorAccountId))
                            .Union(primitiveResults.Select(a => a.TargetAccountId))
                            .Distinct();

            var userDictionary = DataContext.Accounts
                                    .Where(a => allUserIds.Contains(a.AccountId))
                                    .Select(a => new { a.AccountId, a.Name })
                                    .ToList()
                                    .ToDictionary(a => a.AccountId, a => a.Name);

            var viewModels = primitiveResults.Select(a => new GiftViewModel
            {
                BoughtbyUser = a.ClaimedByAccountId == null ? null : userDictionary[a.ClaimedByAccountId.Value],
                Comments = a.Comments,
                CreatedByUser = userDictionary[a.CreatorAccountId],
                Description = a.Description,
                GiftId = a.GiftId,
                Name = a.Name,
                Questions = a.Questions,
                TargetUser = userDictionary[a.TargetAccountId],
                ThisIsYourGift = userId == UserId,
                Url = a.Url,
                YouBoughtThisGift = a.ClaimedByAccountId == UserId,
                TargetRequestedThisGift = a.TargetAccountId == userId
            }).ToList();

            var result = new AccountViewModel
            {
                Gifts = viewModels,
                Name = userDictionary[userId]
            };

            return View(result);
        }
    }
}