namespace NewUniversityProejct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondCreate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ratings", "CreatedOn", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ratings", "CreatedOn");
        }
    }
}
