namespace TripApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Notes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TripNotes",
                c => new
                    {
                        NoteID = c.Int(nullable: false, identity: true),
                        NoteName = c.String(),
                    })
                .PrimaryKey(t => t.NoteID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TripNotes");
        }
    }
}
