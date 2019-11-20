using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class Course
    { 
        public int CourseId { get; set; }
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }
        public List<Quiz> Quizzes { get; set; }
        [MaxLength(125)]
        public string UserId { get; set; }
    }
}