namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class activeQuiz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "Active", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "Active");
        }
    }
}
