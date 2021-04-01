namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigraton : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LastTimePlayedDate = c.DateTime(nullable: false),
                        CompletedDate = c.DateTime(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Piece_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pieces", t => t.Piece_Id)
                .Index(t => t.Piece_Id);
            
            CreateTable(
                "dbo.Pieces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Position = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        PlayerColor = c.String(),
                        IsWinner = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Piece_Id", "dbo.Pieces");
            DropForeignKey("dbo.Pieces", "PlayerId", "dbo.Players");
            DropIndex("dbo.Pieces", new[] { "PlayerId" });
            DropIndex("dbo.Games", new[] { "Piece_Id" });
            DropTable("dbo.Players");
            DropTable("dbo.Pieces");
            DropTable("dbo.Games");
        }
    }
}
