namespace Examen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modification : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Questions", "CorrectAnswerId", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Questions", "CorrectAnswerId", c => c.Int(nullable: false));
        }
    }
}
