namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtableTeacherCourse : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserNames", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "UserLastNames", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AspNetUsers", "StudentName");
            DropColumn("dbo.AspNetUsers", "StudentLastName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "StudentLastName", c => c.String(nullable: false, maxLength: 100));
            AddColumn("dbo.AspNetUsers", "StudentName", c => c.String(nullable: false, maxLength: 100));
            DropColumn("dbo.AspNetUsers", "UserLastNames");
            DropColumn("dbo.AspNetUsers", "UserNames");
            DropColumn("dbo.Quizs", "UserId");
        }
    }
}
