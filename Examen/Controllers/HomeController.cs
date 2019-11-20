using Examen.Models;
using Examen.ViewModel;
using System.Web.Mvc;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using System;
using System.Text;
using System.Collections.Generic;
using Examen.Utility;

namespace Examen.Controllers
{
    [Authorize(Roles = nameof(UserRoles.Teacher) + ", " + nameof(UserRoles.Administrator) + ", " + nameof(UserRoles.Student))]
    public class HomeController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            //TODO Navigate to edit test page
            if (User.IsInRole(UserRoles.Teacher.ToString()))
            {
                return RedirectToAction("Index", "Courses");
            }
      
            return View();
        }


        [HttpPost]
        public ActionResult Index(StudentCourseViewModel info)
        {

            if (ModelState.IsValid)
            {
                //verifica que el quiz este activo y sea codigo valido
                var quiz = _context.TQuizz.FirstOrDefault(q => q.QuizCode == info.QuizCode && q.Active);

                if (quiz== null)
                {
                    return RedirectToAction("Index", "Home");
                }

                var studentCourse = new StudentCourse
                {
                    StudentId = User.Identity.GetUserId(),
                    Class = info.Class,
                    CourseId = quiz.CourseId
                };

                //todo:only add if doesnt exist check first
                var checkIfExist = _context.TStudentCourse.Any(q => q.StudentId == studentCourse.StudentId && q.CourseId == studentCourse.CourseId);

                if (!checkIfExist)
                {
                    _context.TStudentCourse.Add(studentCourse);
                    _context.SaveChanges();
                }

                return RedirectToAction("TakeQuiz", "TakeQuiz",new { id=quiz.QuizId});
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

           
        }

        

    }
}