namespace SmartStore.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableReview : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Url = c.String(),
                        ShortUrl = c.String(),
                        Cover = c.String(),
                        ShortDescription = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        IsHighlight = c.Boolean(nullable: false),
                        IsPremium = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                        UpdatedDate = c.DateTime(nullable: false),
                        VideoUrl = c.String(),
                        Thumbnail = c.String(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "Product_Id", "dbo.Product");
            DropIndex("dbo.Reviews", new[] { "Product_Id" });
            DropTable("dbo.Reviews");
        }
    }
}
