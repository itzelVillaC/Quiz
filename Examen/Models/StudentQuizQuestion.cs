using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class StudentQuizQuestion
    {
        public int StudentQuizQuestionId { get; set; }
        public int? StudentQuizId { get; set; }
        public StudentQuiz StudentQuiz { get; set; }
        public int? QuestionId { get; set; }
        public Question Question { get; set; }
        public int? SelectedAnswerId { get; set; }
        public int? NextStudentQuestionId { get; set; }
    }
}