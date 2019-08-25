namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameUserIdToApplicatorIdInApplyForJobTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ApplyForJobs", name: "UserId", newName: "ApplicatorId");
            RenameIndex(table: "dbo.ApplyForJobs", name: "IX_UserId", newName: "IX_ApplicatorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ApplyForJobs", name: "IX_ApplicatorId", newName: "IX_UserId");
            RenameColumn(table: "dbo.ApplyForJobs", name: "ApplicatorId", newName: "UserId");
        }
    }
}
