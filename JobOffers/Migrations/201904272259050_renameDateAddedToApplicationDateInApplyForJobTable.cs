namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameDateAddedToApplicationDateInApplyForJobTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplyForJobs", "ApplicationDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.ApplyForJobs", "DateAdded");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplyForJobs", "DateAdded", c => c.DateTime(nullable: false));
            DropColumn("dbo.ApplyForJobs", "ApplicationDate");
        }
    }
}
