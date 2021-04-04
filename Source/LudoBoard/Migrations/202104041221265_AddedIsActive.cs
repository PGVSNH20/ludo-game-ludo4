namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIsActive : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pieces", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pieces", "IsActive");
        }
    }
}
