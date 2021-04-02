namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_WinnerPlayerName_Removed_IsWinner : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "WinnerPlayerName", c => c.String());
            DropColumn("dbo.Players", "IsWinner");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Players", "IsWinner", c => c.Boolean(nullable: false));
            DropColumn("dbo.Games", "WinnerPlayerName");
        }
    }
}
