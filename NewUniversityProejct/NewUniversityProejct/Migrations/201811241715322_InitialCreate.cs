namespace NewUniversityProejct.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        ReleaseYear = c.Int(nullable: false),
                        GenreId = c.Int(),
                        RatingId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .ForeignKey("dbo.Ratings", t => t.RatingId)
                .Index(t => t.GenreId)
                .Index(t => t.RatingId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenreName = c.String(nullable: false, maxLength: 100),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Ratings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RatingValue = c.String(nullable: false, maxLength: 100),
                        Description = c.String(maxLength: 400),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "RatingId", "dbo.Ratings");
            DropForeignKey("dbo.Games", "GenreId", "dbo.Genres");
            DropIndex("dbo.Games", new[] { "RatingId" });
            DropIndex("dbo.Games", new[] { "GenreId" });
            DropTable("dbo.Ratings");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
        }
    }
}
