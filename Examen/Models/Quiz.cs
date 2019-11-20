using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Quiz
    {
        public int QuizId { get; set; }
        public string QuizName { get; set; }
        public TimeSpan Duration { get; set; }
        public List<Question> Questions { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string QuizCode { get; set; }
        [MaxLength(125)]
        public string UserId { get; set; }
        public bool Active { get; set; }
    }
}