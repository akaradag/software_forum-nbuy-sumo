namespace BilgeAdam___Sumo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User_Rosette", "RosetteId", "dbo.Rosette");
            DropForeignKey("dbo.User_Rosette", "UserId", "dbo.User");
            DropIndex("dbo.User_Rosette", new[] { "RosetteId" });
            DropIndex("dbo.User_Rosette", new[] { "UserId" });
            CreateTable(
                "dbo.UserRosette",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RosetteId = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Rosette", t => t.RosetteId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RosetteId);
            
            DropTable("dbo.User_Rosette");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.User_Rosette",
                c => new
                    {
                        RosetteId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RosetteId, t.UserId });
            
            DropForeignKey("dbo.UserRosette", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRosette", "RosetteId", "dbo.Rosette");
            DropIndex("dbo.UserRosette", new[] { "RosetteId" });
            DropIndex("dbo.UserRosette", new[] { "UserId" });
            DropTable("dbo.UserRosette");
            CreateIndex("dbo.User_Rosette", "UserId");
            CreateIndex("dbo.User_Rosette", "RosetteId");
            AddForeignKey("dbo.User_Rosette", "UserId", "dbo.User", "Id", cascadeDelete: true);
            AddForeignKey("dbo.User_Rosette", "RosetteId", "dbo.Rosette", "Id", cascadeDelete: true);
        }
    }
}
