namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddJobsCategoryTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Jobs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        JobTitle = c.String(nullable: false),
                        JobDescriptiont = c.String(nullable: false),
                        ImageUrl = c.String(nullable: false),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobCategories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            DropTable("dbo.Jobs");
        }
    }
}
