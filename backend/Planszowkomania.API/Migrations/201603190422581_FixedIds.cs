namespace Planszowkomania.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixedIds : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tables", "Game_Id", "dbo.Games");
            DropIndex("dbo.Tables", new[] { "Game_Id" });
            RenameColumn(table: "dbo.Tables", name: "Game_Id", newName: "GameId");
            RenameColumn(table: "dbo.Tables", name: "Owner_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.Tables", name: "IX_Owner_Id", newName: "IX_OwnerId");
            AlterColumn("dbo.Tables", "GameId", c => c.Int(nullable: false));
            CreateIndex("dbo.Tables", "GameId");
            AddForeignKey("dbo.Tables", "GameId", "dbo.Games", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tables", "GameId", "dbo.Games");
            DropIndex("dbo.Tables", new[] { "GameId" });
            AlterColumn("dbo.Tables", "GameId", c => c.Int());
            RenameIndex(table: "dbo.Tables", name: "IX_OwnerId", newName: "IX_Owner_Id");
            RenameColumn(table: "dbo.Tables", name: "OwnerId", newName: "Owner_Id");
            RenameColumn(table: "dbo.Tables", name: "GameId", newName: "Game_Id");
            CreateIndex("dbo.Tables", "Game_Id");
            AddForeignKey("dbo.Tables", "Game_Id", "dbo.Games", "Id");
        }
    }
}
