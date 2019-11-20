using Examen.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Examen.ViewModel
{
    public class StudentCourseViewModel
    {
        [Required]
        public string Class { get; set; }
        [Required]
        public string QuizCode { get; set; }
    }
}