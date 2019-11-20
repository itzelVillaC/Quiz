using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.ViewModel
{
    public class AddQuestionViewModel
    {
         
        public string QuestionText { get; set; }
        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>();
    }
}