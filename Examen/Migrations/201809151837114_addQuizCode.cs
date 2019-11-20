namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addQuizCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "QuizCode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "QuizCode");
        }
    }
}
