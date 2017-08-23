namespace BilgeAdam___Sumo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.UserRosette");
            AlterColumn("dbo.UserRosette", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.UserRosette", new[] { "UserId", "RosetteId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.UserRosette");
            AlterColumn("dbo.UserRosette", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.UserRosette", "Id");
        }
    }
}
