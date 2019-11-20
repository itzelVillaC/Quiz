namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.StudentCourses", "Class", c => c.String());
            CreateIndex("dbo.StudentCourses", "CourseId");
            AddForeignKey("dbo.StudentCourses", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StudentCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.StudentCourses", new[] { "CourseId" });
            DropColumn("dbo.StudentCourses", "Class");
        }
    }
}
