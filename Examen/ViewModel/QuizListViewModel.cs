using Examen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.ViewModel
{
    public class QuizListViewModel
    {
        public int QuizId { get; set; }   
        [Display (Name = "Quiz Name")]
        public string QuizName { get; set; }
        [Display(Name = "Quiz Code")]
        public string QuizCode { get; set; }
        public bool Active { get; set; }
    }

}