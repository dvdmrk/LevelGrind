namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Fields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Pseudonym", c => c.String());
            AddColumn("dbo.AspNetUsers", "Feet", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Inches", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Weight", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Weight");
            DropColumn("dbo.AspNetUsers", "Inches");
            DropColumn("dbo.AspNetUsers", "Feet");
            DropColumn("dbo.AspNetUsers", "Pseudonym");
        }
    }
}
