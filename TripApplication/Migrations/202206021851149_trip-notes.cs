namespace TripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tripnotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Trips", "NoteID", c => c.Int(nullable: false));
            CreateIndex("dbo.Trips", "NoteID");
            AddForeignKey("dbo.Trips", "NoteID", "dbo.TripNotes", "NoteID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Trips", "NoteID", "dbo.TripNotes");
            DropIndex("dbo.Trips", new[] { "NoteID" });
            DropColumn("dbo.Trips", "NoteID");
        }
    }
}
