namespace Planszowkomania.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CorrectedParticipations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Participations", "Table_Id", "dbo.Tables");
            DropIndex("dbo.Participations", new[] { "Table_Id" });
            RenameColumn(table: "dbo.Participations", name: "Participant_Id", newName: "ParticipantId");
            RenameColumn(table: "dbo.Participations", name: "Table_Id", newName: "TableId");
            RenameIndex(table: "dbo.Participations", name: "IX_Participant_Id", newName: "IX_ParticipantId");
            AlterColumn("dbo.Participations", "TableId", c => c.Int(nullable: false));
            CreateIndex("dbo.Participations", "TableId");
            AddForeignKey("dbo.Participations", "TableId", "dbo.Tables", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Participations", "TableId", "dbo.Tables");
            DropIndex("dbo.Participations", new[] { "TableId" });
            AlterColumn("dbo.Participations", "TableId", c => c.Int());
            RenameIndex(table: "dbo.Participations", name: "IX_ParticipantId", newName: "IX_Participant_Id");
            RenameColumn(table: "dbo.Participations", name: "TableId", newName: "Table_Id");
            RenameColumn(table: "dbo.Participations", name: "ParticipantId", newName: "Participant_Id");
            CreateIndex("dbo.Participations", "Table_Id");
            AddForeignKey("dbo.Participations", "Table_Id", "dbo.Tables", "Id");
        }
    }
}
