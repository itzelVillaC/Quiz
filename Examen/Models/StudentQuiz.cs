using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class StudentQuiz
    {
        public int StudentQuizId { get; set; }
        public string StudentId { get; set; }
        public int QuizId { get; set; }
        public List<StudentQuizQuestion> StudentQuizQuestions { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Decimal? FinalScore { get; set; }
    }
}