using Examen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.ViewModel
{
    public class CourseListViewModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public List<QuizListViewModel> Quiz { get; set; }
    }
}