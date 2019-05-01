namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTablesReviewerProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviewers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ChannelName = c.String(),
                        RegisterdDate = c.DateTime(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.ReviewImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayOrder = c.Int(nullable: false),
                        Picture_Id = c.Int(),
                        Review_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Picture", t => t.Picture_Id)
                .ForeignKey("dbo.Reviews", t => t.Review_Id)
                .Index(t => t.Picture_Id)
                .Index(t => t.Review_Id);
            
            AddColumn("dbo.Reviews", "Reviewer_Id", c => c.Int());
            CreateIndex("dbo.Reviews", "Reviewer_Id");
            AddForeignKey("dbo.Reviews", "Reviewer_Id", "dbo.Reviewers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReviewImages", "Review_Id", "dbo.Reviews");
            DropForeignKey("dbo.ReviewImages", "Picture_Id", "dbo.Picture");
            DropForeignKey("dbo.Reviewers", "User_Id", "dbo.Customer");
            DropForeignKey("dbo.Reviews", "Reviewer_Id", "dbo.Reviewers");
            DropIndex("dbo.ReviewImages", new[] { "Review_Id" });
            DropIndex("dbo.ReviewImages", new[] { "Picture_Id" });
            DropIndex("dbo.Reviewers", new[] { "User_Id" });
            DropIndex("dbo.Reviews", new[] { "Reviewer_Id" });
            DropColumn("dbo.Reviews", "Reviewer_Id");
            DropTable("dbo.ReviewImages");
            DropTable("dbo.Reviewers");
        }
    }
}
