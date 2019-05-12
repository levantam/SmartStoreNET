namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreviewlanguage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Review", "Language_Id", c => c.Int());
            CreateIndex("dbo.Review", "Language_Id");
            AddForeignKey("dbo.Review", "Language_Id", "dbo.Language", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "Language_Id", "dbo.Language");
            DropIndex("dbo.Review", new[] { "Language_Id" });
            DropColumn("dbo.Review", "Language_Id");
        }
    }
}
