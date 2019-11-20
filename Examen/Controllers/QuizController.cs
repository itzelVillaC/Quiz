using Examen.Models;
using Examen.Utility;
using Examen.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Examen.Controllers
{
    //access role
    [Authorize(Roles = nameof(UserRoles.Teacher) + ", " + nameof(UserRoles.Administrator))]
    public class QuizController : Controller
    {
        // GET: Quiz
        private ApplicationDbContext _context=new ApplicationDbContext();
        public ActionResult Create()
        {
            var userId = User.Identity.GetUserId();
            //todo: course that belong to the user list
            var viewModel = new QuizInformationViewModel
            {
                Courses = _context.TCourse.Where(c=>c.UserId==userId).ToList()

            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(QuizInformationViewModel information)
        {
            var quizCreation = new Quiz
            {
                QuizName = information.QuizName,
                CourseId = information.CourseId,
                Active=true,
                QuizCode = CodeGenerator()

            };
            _context.TQuizz.Add(quizCreation);
            _context.SaveChanges();
            return RedirectToAction("CreateQuestion", new {id= quizCreation.QuizId} );
        }


        public ActionResult CreateQuestion(int Id)
        {
            var quizInformation = _context.TQuizz.Include(q =>q.Questions.Select(a=>a.Answers)).Where(q => q.QuizId == Id).FirstOrDefault();
     
            var viewModel = new QuizInformationViewModel
            {
                QuizId=quizInformation.QuizId,
                QuizName=quizInformation.QuizName,
                QuizCode=quizInformation.QuizCode,
                Questions = quizInformation.Questions.Select(q => new AddQuestionViewModel
                {
                    QuestionText = q.QuestionText,
                    Answers = q.Answers.Select(a => new AnswerViewModel { AnswerText = a.AnswerText }).ToList()
                }).ToList()
                
            };

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateQuestion( QuizInformationViewModel information, string[] DynamicTextBox)
        {
            var addQuestion = new Question
            {
                QuestionText = information.QuestionText,
                QuizId = information.QuizId,
                Answers = new List<Answer>()
            };

            foreach (var item in DynamicTextBox)
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    addQuestion.Answers.Add(
                    new Answer
                    {
                      
                        AnswerText = item
                    });
                }
            }
            _context.TQuestion.Add(addQuestion);
            _context.SaveChanges();

            addQuestion.CorrectAnswerId = addQuestion.Answers.First().AnswerId;
            _context.SaveChanges();
            return RedirectToAction ("CreateQuestion", new { id = information.QuizId });
        }

        public ActionResult ListQuizes()
        {
            var userId = User.Identity.GetUserId();
            var tempo = _context.TCourse.Include(q => q.Quizzes).Where(c => c.UserId == userId).ToList();
            var list = new List<CourseListViewModel>();

            foreach (var item in tempo)
            {
                list.Add(new CourseListViewModel
                {
                    CourseId = item.CourseId,
                    CourseName = item.CourseName,
                    Quiz = item.Quizzes.Where(q=>q.Active).Select(q => new QuizListViewModel{ QuizId=q.QuizId,QuizName=q.QuizName,QuizCode=q.QuizCode}).ToList()
                });            
            }
            return View(list);
        }


        public ActionResult DeleteQuiz(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var infoQuiz = _context.TQuizz.Find(id);
            if (infoQuiz == null)
            {
                return HttpNotFound();
            }

            var q = new QuizListViewModel
            {
                QuizId= infoQuiz.QuizId,
                QuizName= infoQuiz.QuizName,
                QuizCode= infoQuiz.QuizCode,
                Active=infoQuiz.Active
              
            };        
            return View(q);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteQuiz(QuizListViewModel info)
        {
            var quiz = _context.TQuizz.Find(info.QuizId);
            quiz.Active = false;
            _context.SaveChanges();
            return RedirectToAction("ListQuizes");
        }


        public string CodeGenerator()
        {
            var rand = new Random();
            var code = new List<char>();
            for (var i = 0; i < 5; i++)
            {
                var asciiCode = rand.Next(97, 122);
                code.Add((char)asciiCode);
            }
            for (var i = 0; i < 3; i++)
            {
                var asciiCode = rand.Next(48, 57);
                code.Add((char)asciiCode);
            }
            code = code.OrderBy(x => rand.Next()).ToList();
            return String.Join("", code);
        }
    }
}