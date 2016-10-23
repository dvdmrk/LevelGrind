namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuestPoints : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "QuestPoints", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "QuestPoints");
        }
    }
}
