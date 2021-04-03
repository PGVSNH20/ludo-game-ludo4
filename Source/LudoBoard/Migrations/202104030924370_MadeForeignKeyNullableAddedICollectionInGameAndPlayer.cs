namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeForeignKeyNullableAddedICollectionInGameAndPlayer : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Pieces", name: "Player_Id", newName: "PlayerId");
            RenameColumn(table: "dbo.Players", name: "GameId_Id", newName: "GameId");
            RenameIndex(table: "dbo.Players", name: "IX_GameId_Id", newName: "IX_GameId");
            RenameIndex(table: "dbo.Pieces", name: "IX_Player_Id", newName: "IX_PlayerId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Pieces", name: "IX_PlayerId", newName: "IX_Player_Id");
            RenameIndex(table: "dbo.Players", name: "IX_GameId", newName: "IX_GameId_Id");
            RenameColumn(table: "dbo.Players", name: "GameId", newName: "GameId_Id");
            RenameColumn(table: "dbo.Pieces", name: "PlayerId", newName: "Player_Id");
        }
    }
}
