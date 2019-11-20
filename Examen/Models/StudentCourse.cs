using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Examen.Models
{
    public class StudentCourse
    {
        public int StudentCourseId { get; set; }
        public string StudentId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string Class { get; set; }
    }
}