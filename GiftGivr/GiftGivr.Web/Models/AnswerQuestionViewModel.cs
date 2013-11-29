using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GiftGivr.Web.Models
{
    public class AnswerQuestionViewModel
    {
        public string Gift { get; set; }
        public string GiftCreator { get; set; }

        public string Question { get; set; }
        public string QuestionAsker { get; set; }

        public string Answer { get; set; }
    }
}