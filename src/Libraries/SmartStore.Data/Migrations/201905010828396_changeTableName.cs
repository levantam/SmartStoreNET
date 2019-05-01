namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeTableName : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Reviews", newName: "Review");
            RenameTable(name: "dbo.Reviewers", newName: "Reviewer");
            RenameTable(name: "dbo.ReviewImages", newName: "ReviewImage");
            AlterColumn("dbo.Review", "Name", c => c.String(maxLength: 200));
            AlterColumn("dbo.Review", "Url", c => c.String(maxLength: 400));
            AlterColumn("dbo.Review", "ShortUrl", c => c.String(maxLength: 100));
            AlterColumn("dbo.Review", "Cover", c => c.String(maxLength: 100));
            AlterColumn("dbo.Review", "ShortDescription", c => c.String(maxLength: 500));
            AlterColumn("dbo.Review", "Thumbnail", c => c.String(maxLength: 400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Review", "Thumbnail", c => c.String());
            AlterColumn("dbo.Review", "ShortDescription", c => c.String());
            AlterColumn("dbo.Review", "Cover", c => c.String());
            AlterColumn("dbo.Review", "ShortUrl", c => c.String());
            AlterColumn("dbo.Review", "Url", c => c.String());
            AlterColumn("dbo.Review", "Name", c => c.String());
            RenameTable(name: "dbo.ReviewImage", newName: "ReviewImages");
            RenameTable(name: "dbo.Reviewer", newName: "Reviewers");
            RenameTable(name: "dbo.Review", newName: "Reviews");
        }
    }
}
