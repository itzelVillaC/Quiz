namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fiellIdUser : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TeacherCourses", "UserId", c => c.String(maxLength: 125));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TeacherCourses", "UserId", c => c.Int(nullable: false));
        }
    }
}
