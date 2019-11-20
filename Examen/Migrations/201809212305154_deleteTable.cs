namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeacherCourses", "CourseId", "dbo.Courses");
            DropIndex("dbo.TeacherCourses", new[] { "CourseId" });
            AddColumn("dbo.Courses", "UserId", c => c.String(maxLength: 125));
            DropTable("dbo.TeacherCourses");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeacherCourses",
                c => new
                    {
                        TeacherCourseId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 125),
                    })
                .PrimaryKey(t => t.TeacherCourseId);
            
            DropColumn("dbo.Courses", "UserId");
            CreateIndex("dbo.TeacherCourses", "CourseId");
            AddForeignKey("dbo.TeacherCourses", "CourseId", "dbo.Courses", "CourseId", cascadeDelete: true);
        }
    }
}
