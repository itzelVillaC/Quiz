using Examen.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Examen.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Question> TQuestion { get; set; }
        public DbSet<Answer> TAnswer { get; set; }
        public DbSet<Quiz> TQuizz { get; set; }
        public DbSet<Course> TCourse { get; set; }
        public DbSet<StudentQuiz> TStudentQuiz { get; set; }
        public DbSet<StudentQuizQuestion> TStudentQuizQuestion { get; set; }
        public DbSet<StudentCourse> TStudentCourse { get; set; }
       
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        }
}