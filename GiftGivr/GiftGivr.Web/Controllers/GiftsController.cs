using GiftGivr.Web.Classes;
using GiftGivr.Web.Models;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GiftGivr.Web.Controllers
{
    public class GiftsController : BaseController
    {
        public GiftsController(GiftGivrControllerContext context)
            : base(context) { }

        [HttpGet]
        public ActionResult View(int userId)
        {
            var primitiveResults = (from g in DataContext.Gifts
                                    where g.TargetAccountId == userId
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
                                            QuestionId = a.GiftQuestionId,
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
                            .Union(new[] { userId })
                            .Distinct();

            var userDictionary = DataContext.Accounts
                                    .Where(a => allUserIds.Contains(a.AccountId))
                                    .Select(a => new { a.AccountId, a.Name })
                                    .ToList()
                                    .ToDictionary(a => a.AccountId, a => a.Name);

            var viewModels = primitiveResults.Select(a => new GiftViewModel
            {
                BoughtByUser = a.ClaimedByAccountId == null ? null : userDictionary[a.ClaimedByAccountId.Value],
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
                TargetRequestedThisGift = a.TargetAccountId == a.CreatorAccountId
            }).ToList();

            var result = new GiftCollectionViewModel
            {
                UserId = userId,
                ThisIsYou = userId == UserId,
                Gifts = viewModels,
                Name = userDictionary[userId]
            };

            return View(result);
        }

        [HttpGet]
        public ActionResult Create(int userId)
        {
            var user = DataContext.Accounts.Single(a => a.AccountId == userId);
            return View(new GiftViewModel
            {
                TargetUser = user.Name
            });
        }

        [HttpPost]
        public ActionResult Create(int userId, GiftViewModel model)
        {
            var dbItem = new Data.Gift
            {
                CreatorAccountId = UserId.Value,
                TargetAccountId = userId,
                Name = model.Name,
                Description = model.Description,
                PurchaseUrl = model.Url
            };

            DataContext.Gifts.InsertOnSubmit(dbItem);
            DataContext.SubmitChanges();

            return RedirectToAction("View", new { userId });
        }

        [HttpGet]
        public ActionResult Claim(int giftId)
        {
            var gift = DataContext.Gifts.Single(a => a.GiftId == giftId);
            var viewModel = new ClaimGiftViewModel
            {
                Claim = true,
                Comment = null,
                Gift = gift.Name,
                GiftCreator = DataContext.Accounts.Single(a => a.AccountId == gift.CreatorAccountId).Name,
                GiftTarget = DataContext.Accounts.Single(a => a.AccountId == gift.TargetAccountId).Name
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Claim(int giftId, ClaimGiftViewModel model)
        {
            var gift = DataContext.Gifts.Single(a => a.GiftId == giftId);
            if (model.Claim && gift.ClaimedByAccountId == null)
            {
                gift.ClaimedByAccountId = UserId.Value;
            }
            else if (!model.Claim && gift.ClaimedByAccountId == UserId.Value)
            {
                gift.ClaimedByAccountId = null;
            }
            if (model.Comment.IsNotBlank())
            {
                var comment = new Data.Comment
                {
                    AccountId = UserId.Value,
                    CommentText = model.Comment,
                    GiftId = giftId,
                    Timestamp = DateTime.UtcNow
                };
                DataContext.Comments.InsertOnSubmit(comment);
            }
            DataContext.SubmitChanges();

            return RedirectToAction("View", new { userId = gift.TargetAccountId });
        }
    }
}
