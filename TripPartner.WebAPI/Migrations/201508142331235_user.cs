namespace TripPartner.WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Stories", "Rating", c => c.Double(nullable: false));
            AddColumn("dbo.Stories", "Rates", c => c.Int(nullable: false));
            AddColumn("dbo.Stories", "Title", c => c.String());
            CreateIndex("dbo.Stories", "DateMade", name: "DateMade");
            CreateIndex("dbo.Stories", "Rating", name: "Rating");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Stories", "Rating");
            DropIndex("dbo.Stories", "DateMade");
            DropColumn("dbo.Stories", "Title");
            DropColumn("dbo.Stories", "Rates");
            DropColumn("dbo.Stories", "Rating");
        }
    }
}
