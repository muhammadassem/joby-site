namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameByteToInt : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jobs");
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropIndex("dbo.ApplyForJobs", new[] { "JobId" });
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            DropPrimaryKey("dbo.ApplyForJobs");
            DropPrimaryKey("dbo.Jobs");
            DropPrimaryKey("dbo.JobCategories");
            AlterColumn("dbo.ApplyForJobs", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ApplyForJobs", "JobId", c => c.Int(nullable: false));
            AlterColumn("dbo.Jobs", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Int(nullable: false));
            AlterColumn("dbo.JobCategories", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ApplyForJobs", "Id");
            AddPrimaryKey("dbo.Jobs", "Id");
            AddPrimaryKey("dbo.JobCategories", "Id");
            CreateIndex("dbo.ApplyForJobs", "JobId");
            CreateIndex("dbo.Jobs", "CategoryId");
            AddForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories");
            DropForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jobs");
            DropIndex("dbo.Jobs", new[] { "CategoryId" });
            DropIndex("dbo.ApplyForJobs", new[] { "JobId" });
            DropPrimaryKey("dbo.JobCategories");
            DropPrimaryKey("dbo.Jobs");
            DropPrimaryKey("dbo.ApplyForJobs");
            AlterColumn("dbo.JobCategories", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Jobs", "CategoryId", c => c.Byte(nullable: false));
            AlterColumn("dbo.Jobs", "Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.ApplyForJobs", "JobId", c => c.Byte(nullable: false));
            AlterColumn("dbo.ApplyForJobs", "Id", c => c.Byte(nullable: false));
            AddPrimaryKey("dbo.JobCategories", "Id");
            AddPrimaryKey("dbo.Jobs", "Id");
            AddPrimaryKey("dbo.ApplyForJobs", "Id");
            CreateIndex("dbo.Jobs", "CategoryId");
            CreateIndex("dbo.ApplyForJobs", "JobId");
            AddForeignKey("dbo.Jobs", "CategoryId", "dbo.JobCategories", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplyForJobs", "JobId", "dbo.Jobs", "Id", cascadeDelete: true);
        }
    }
}
