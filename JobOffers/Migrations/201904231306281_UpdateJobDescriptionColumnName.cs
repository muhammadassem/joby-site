namespace JobOffers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateJobDescriptionColumnName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jobs", "JobDescription", c => c.String(nullable: false));
            DropColumn("dbo.Jobs", "JobDescriptiont");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Jobs", "JobDescriptiont", c => c.String(nullable: false));
            DropColumn("dbo.Jobs", "JobDescription");
        }
    }
}
