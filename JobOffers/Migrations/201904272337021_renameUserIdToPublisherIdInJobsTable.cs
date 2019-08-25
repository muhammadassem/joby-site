namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameUserIdToPublisherIdInJobsTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Jobs", name: "UserId", newName: "PublisherId");
            RenameIndex(table: "dbo.Jobs", name: "IX_UserId", newName: "IX_PublisherId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Jobs", name: "IX_PublisherId", newName: "IX_UserId");
            RenameColumn(table: "dbo.Jobs", name: "PublisherId", newName: "UserId");
        }
    }
}
