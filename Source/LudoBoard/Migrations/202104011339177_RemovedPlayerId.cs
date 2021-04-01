namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedPlayerId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pieces", "PlayerId", "dbo.Players");
            DropIndex("dbo.Pieces", new[] { "PlayerId" });
            RenameColumn(table: "dbo.Pieces", name: "PlayerId", newName: "Player_Id");
            AlterColumn("dbo.Pieces", "Player_Id", c => c.Int());
            CreateIndex("dbo.Pieces", "Player_Id");
            AddForeignKey("dbo.Pieces", "Player_Id", "dbo.Players", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pieces", "Player_Id", "dbo.Players");
            DropIndex("dbo.Pieces", new[] { "Player_Id" });
            AlterColumn("dbo.Pieces", "Player_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Pieces", name: "Player_Id", newName: "PlayerId");
            CreateIndex("dbo.Pieces", "PlayerId");
            AddForeignKey("dbo.Pieces", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
