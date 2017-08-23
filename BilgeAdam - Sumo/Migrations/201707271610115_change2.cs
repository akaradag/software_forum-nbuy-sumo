namespace BilgeAdam___Sumo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change2 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.UserRosette", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserRosette", "Id", c => c.Int(nullable: false));
        }
    }
}
