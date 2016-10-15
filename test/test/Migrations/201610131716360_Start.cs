namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Start : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "LastTrainedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "LastTrainedOn");
            DropColumn("dbo.AspNetUsers", "StartDate");
        }
    }
}
