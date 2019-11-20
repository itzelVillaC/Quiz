using Examen.Models;
using Examen.Utility;
using Examen.ViewModel;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Examen.Controllers
{
    [Authorize(Roles = nameof(UserRoles.Teacher) + ", " + nameof(UserRoles.Administrator) + ", " + nameof(UserRoles.Student))]
    public class TakeQuizController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();
        // GET: TakeQuiz


        [Authorize]
        public ActionResult TakeQuiz(int id)
        {
            var quiz = _context.TQuizz.Include(r => r.Questions).FirstOrDefault(q => q.QuizId == id);
            var userId = User.Identity.GetUserId();

            if (quiz == null || userId == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var checkIfExits = _context.TStudentQuiz.FirstOrDefault(r => r.StudentId == userId && r.QuizId == quiz.QuizId);

            if (checkIfExits != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var LoadQuiz = new StudentQuiz()
                {
                    QuizId = quiz.QuizId,
                    StudentId = userId,
                    StartTime = DateTime.Now

                };
                _context.TStudentQuiz.Add(LoadQuiz);
                _context.SaveChanges();

                var listQuiz = new List<StudentQuizQuestion>();
                var questions = quiz.Questions;

                foreach (var q in questions)
                {

                    var addQuestion = new StudentQuizQuestion
                    {
                        StudentQuizId = LoadQuiz.StudentQuizId,
                        QuestionId = q.QuestionId
                    };

                    listQuiz.Add(addQuestion);
                    _context.TStudentQuizQuestion.Add(addQuestion);
                    _context.SaveChanges();

                }
                // random listQuiz
                Random random = new Random();
                var ListaRandom = listQuiz.OrderBy(x => random.Next()).ToList();

                for (int i = 0; i < ListaRandom.Count; i++)
                {
                    try
                    {
                        ListaRandom[i].NextStudentQuestionId = ListaRandom[i + 1].StudentQuizQuestionId;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        //expected exception
                    }
                }
                _context.SaveChanges();
                return RedirectToAction("QuizQuestion", new { id = ListaRandom.First().StudentQuizQuestionId });
            }
        }

        public ActionResult QuizQuestion(int id)
        {
            var temp = _context.TStudentQuizQuestion.Include(r => r.Question).FirstOrDefault(x => x.StudentQuizQuestionId == id);
            var respuestasTemp = _context.TAnswer.Where(x => x.QuestionId == temp.QuestionId);

            var viewModel = new QuestionsChoiceViewModel
            {
                StudentQuizQuestionId = temp.StudentQuizQuestionId,
                QuestionText = temp.Question.QuestionText,
                Answers = respuestasTemp.ToList()
            };
            return View(viewModel);
        }


        [HttpPost]
        public ActionResult QuizQuestion(QuestionsChoiceViewModel choice)
        {
            var currentRecord = _context.TStudentQuizQuestion.
                Where(x => x.StudentQuizQuestionId == choice.StudentQuizQuestionId).FirstOrDefault();
            //si no es null no a finaliazado la prueba
            if (currentRecord.NextStudentQuestionId != null)
            {
                currentRecord.SelectedAnswerId = choice.SelectedAnswerId;
                _context.SaveChanges();
            }
            else
            {
                currentRecord.SelectedAnswerId = choice.SelectedAnswerId;
                _context.SaveChanges();

                //save record  when student finish quiz in studenQuiz
                var studentQuiz = _context.TStudentQuiz.Include(q => q.StudentQuizQuestions).
                    Include(q => q.StudentQuizQuestions.Select(z => z.Question)).
                    Where(s => s.StudentQuizId == currentRecord.StudentQuizId).FirstOrDefault();

                var correctAnswers = studentQuiz.StudentQuizQuestions.Where(q => q.SelectedAnswerId == q.Question.CorrectAnswerId).Count();
                var totalQuestions = studentQuiz.StudentQuizQuestions.Count();
                var finalScore = (correctAnswers * 100) / totalQuestions;
                studentQuiz.FinalScore = finalScore;
                studentQuiz.EndTime = DateTime.Now;
                _context.SaveChanges();
                return RedirectToAction("QuizResult", new { id = studentQuiz.StudentQuizId });
            }
            return RedirectToAction("QuizQuestion", new { id = currentRecord.NextStudentQuestionId });
        }


        public ActionResult QuizResult(int id)
        {
            var viewModel = _context.TStudentQuiz.Where(sq => sq.StudentQuizId == id).FirstOrDefault();
            return View(viewModel);
        }
    }
}