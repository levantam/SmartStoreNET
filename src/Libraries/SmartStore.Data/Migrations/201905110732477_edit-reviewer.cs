namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editreviewer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviewer", "UserName", c => c.String());
            AddColumn("dbo.Reviewer", "ShortReview", c => c.String());
            AddColumn("dbo.Reviewer", "IsCrawlData", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reviewer", "LastCrawlDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Reviewer", "YoutubeId", c => c.String());
            AddColumn("dbo.Reviewer", "CanPost", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reviewer", "IsOwner", c => c.Boolean(nullable: false));
            AddColumn("dbo.Reviewer", "Language_Id", c => c.Int());
            AlterColumn("dbo.Review", "CreatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Review", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Reviewer", "RegisterdDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Reviewer", "Language_Id");
            AddForeignKey("dbo.Reviewer", "Language_Id", "dbo.Language", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviewer", "Language_Id", "dbo.Language");
            DropIndex("dbo.Reviewer", new[] { "Language_Id" });
            AlterColumn("dbo.Reviewer", "RegisterdDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Review", "UpdatedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Review", "CreatedDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Reviewer", "Language_Id");
            DropColumn("dbo.Reviewer", "IsOwner");
            DropColumn("dbo.Reviewer", "CanPost");
            DropColumn("dbo.Reviewer", "YoutubeId");
            DropColumn("dbo.Reviewer", "LastCrawlDate");
            DropColumn("dbo.Reviewer", "IsCrawlData");
            DropColumn("dbo.Reviewer", "ShortReview");
            DropColumn("dbo.Reviewer", "UserName");
        }
    }
}
