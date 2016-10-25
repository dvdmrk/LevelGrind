namespace test.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Questss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Quests",
                c => new
                    {
                        QuestsId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        GroupName = c.String(),
                        AlsoQuesting = c.String(),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.QuestsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Quests");
        }
    }
}
