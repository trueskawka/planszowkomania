namespace Planszowkomania.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedImageToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Image", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Image");
        }
    }
}
