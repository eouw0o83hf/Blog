using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Models
{
    public class GiftViewModel
    {
        public int GiftId { get; set; }
        public string TargetUser { get; set; }

        public bool TargetRequestedThisGift { get; set; }
        public bool ThisIsYourGift { get; set; }
        public bool YouBoughtThisGift { get; set; }

        public string CreatedByUser { get; set; }
        public string BoughtByUser { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public ICollection<GiftQuestionViewModel> Questions { get; set; }
        public ICollection<GiftCommentViewModel> Comments { get; set; }
    }

    public class GiftQuestionViewModel
    {
        public string User { get; set; }
        public string Question { get; set; }
        public int QuestionId { get; set; }

        public DateTime Timestamp { get; set; }

        public ICollection<GiftAnswerViewModel> Answers { get; set; }
    }

    public class GiftAnswerViewModel
    {
        public string User { get; set; }
        public string Answer { get; set; }

        public DateTime Timestamp { get; set; }
    }

    public class GiftCommentViewModel
    {
        public string User { get; set; }
        public string Comment { get; set; }

        public DateTime Timestamp { get; set; }
    }
}