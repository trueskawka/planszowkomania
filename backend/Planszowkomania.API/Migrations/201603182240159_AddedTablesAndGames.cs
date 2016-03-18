namespace Planszowkomania.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTablesAndGames : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Participations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Participant_Id = c.String(maxLength: 128),
                        Table_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Participant_Id)
                .ForeignKey("dbo.Tables", t => t.Table_Id)
                .Index(t => t.Participant_Id)
                .Index(t => t.Table_Id);
            
            CreateTable(
                "dbo.Tables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        LocalizationName = c.String(),
                        City = c.String(),
                        Difficulty = c.Int(nullable: false),
                        AggresionLevel = c.Int(nullable: false),
                        UsersRequired = c.Int(nullable: false),
                        Game_Id = c.Int(),
                        Owner_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.Game_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id)
                .Index(t => t.Game_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participations", "Table_Id", "dbo.Tables");
            DropForeignKey("dbo.Tables", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tables", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.Participations", "Participant_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tables", new[] { "Owner_Id" });
            DropIndex("dbo.Tables", new[] { "Game_Id" });
            DropIndex("dbo.Participations", new[] { "Table_Id" });
            DropIndex("dbo.Participations", new[] { "Participant_Id" });
            DropTable("dbo.Tables");
            DropTable("dbo.Participations");
            DropTable("dbo.Games");
        }
    }
}
