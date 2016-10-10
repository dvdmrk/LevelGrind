namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class morefields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "InitialWeight", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "CurrentWeight", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "TrainingTotal", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "Weight");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Weight", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "TrainingTotal");
            DropColumn("dbo.AspNetUsers", "CurrentWeight");
            DropColumn("dbo.AspNetUsers", "InitialWeight");
        }
    }
}
