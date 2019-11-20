using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Question
    {
        public int QuestionId { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public List<Answer> Answers { get; set; }
        public int? CorrectAnswerId { get; set; }
        //public Answer CorrectAnswer { get; set; }
        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}