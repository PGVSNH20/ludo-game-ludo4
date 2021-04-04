namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SomthingWentWrong : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "PlayerTurn", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "PlayerTurn");
        }
    }
}
