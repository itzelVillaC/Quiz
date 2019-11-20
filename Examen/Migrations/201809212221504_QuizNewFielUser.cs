namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuizNewFielUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        TeacherCourseId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TeacherCourseId)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.CourseId);
            
            AlterColumn("dbo.Quizs", "UserId", c => c.String(maxLength: 125));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeacherCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TeacherCourses", new[] { "CourseId" });
            AlterColumn("dbo.Quizs", "UserId", c => c.Int(nullable: false));
            DropTable("dbo.TeacherCourses");
        }
    }
}
