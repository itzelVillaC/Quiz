using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.ViewModel
{
    public class QuestionsChoiceViewModel
    {
        public int StudentQuizQuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<Answer> Answers { get; set; }
        public int? SelectedAnswerId { get; set; }
    }
}