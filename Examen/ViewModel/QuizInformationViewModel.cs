using Examen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.ViewModel
{
    public class QuizInformationViewModel
    {
        public int QuizId { get; set; }
        [Display(Name = "Quiz Title")]
        public string QuizName { get; set; }
        [Display(Name = "Select Course")]
        public int CourseId { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        [Display(Name = "Type your Question")]
        public string QuestionText { get; set; }
        [Display(Name = "Type your Answer(s)")]
        public string AnswerText { get; set; }
        public int  SelectedCorrectAnswer { get; set; }
        public List<AddQuestionViewModel> Questions { get; set; } = new List<AddQuestionViewModel>();
        public string QuizCode { get; set; }
    }
}