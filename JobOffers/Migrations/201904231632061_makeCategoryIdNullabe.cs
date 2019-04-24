namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeCategoryIdNullabe : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Int());
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories", "Id", cascadeDelete: true);
        }
    }
}
