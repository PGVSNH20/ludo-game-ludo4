namespace LudoBoard.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Changed_DateTimeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Games", "LastTimePlayedDate", c => c.DateTime());
            AlterColumn("dbo.Games", "CompletedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Games", "CompletedDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Games", "LastTimePlayedDate", c => c.DateTime(nullable: false));
        }
    }
}
