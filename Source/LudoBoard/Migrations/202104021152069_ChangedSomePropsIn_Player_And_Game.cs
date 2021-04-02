namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedSomePropsIn_Player_And_Game : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Games", "Piece_Id", "dbo.Pieces");
            DropIndex("dbo.Games", new[] { "Piece_Id" });
            AddColumn("dbo.Players", "GameId_Id", c => c.Int());
            CreateIndex("dbo.Players", "GameId_Id");
            AddForeignKey("dbo.Players", "GameId_Id", "dbo.Games", "Id");
            DropColumn("dbo.Games", "Piece_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "Piece_Id", c => c.Int());
            DropForeignKey("dbo.Players", "GameId_Id", "dbo.Games");
            DropIndex("dbo.Players", new[] { "GameId_Id" });
            DropColumn("dbo.Players", "GameId_Id");
            CreateIndex("dbo.Games", "Piece_Id");
            AddForeignKey("dbo.Games", "Piece_Id", "dbo.Pieces", "Id");
        }
    }
}
