namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setCategoryIdTobyte : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            DropPrimaryKey("dbo.JobCategories");
            AlterColumn("dbo.JobCategories", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.JobCategories", "Id");
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            DropPrimaryKey("dbo.JobCategories");
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.JobCategories", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.JobCategories", "Id");
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories", "Id", cascadeDelete: true);
        }
    }
}
