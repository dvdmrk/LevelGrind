namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Group : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Group", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Group");
        }
    }
}
