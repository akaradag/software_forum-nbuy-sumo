namespace BilgeAdam___Sumo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        AnswerId = c.Int(),
                        UserId = c.Int(nullable: false),
                        QuestionId = c.Int(nullable: false),
                        Accepted = c.Boolean(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .Index(t => t.AnswerId)
                .Index(t => t.UserId)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.AnswerVote",
                c => new
                    {
                        AnswerId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Vote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.AnswerId, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Answer", t => t.AnswerId)
                .Index(t => t.AnswerId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50, unicode: false),
                        LastName = c.String(nullable: false, maxLength: 50, unicode: false),
                        Mail = c.String(nullable: false, maxLength: 100, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Phone = c.String(maxLength: 50, unicode: false),
                        Country = c.String(maxLength: 50, unicode: false),
                        City = c.String(maxLength: 50, unicode: false),
                        Region = c.String(maxLength: 50, unicode: false),
                        About = c.String(maxLength: 8000, unicode: false),
                        BirthDate = c.DateTime(storeType: "date"),
                        Company = c.String(maxLength: 100, unicode: false),
                        Site = c.String(maxLength: 50, unicode: false),
                        Language = c.String(maxLength: 50, unicode: false),
                        Image = c.Binary(storeType: "image"),
                        RoleId = c.Int(nullable: false),
                        RepPoint = c.Int(nullable: false),
                        AccountCreateDate = c.DateTime(nullable: false, storeType: "date"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Role", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ArticleFollow",
                c => new
                    {
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.UserId })
                .ForeignKey("dbo.Article", t => t.ArticleId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Article",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        ViewCount = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.ArticleVote",
                c => new
                    {
                        ArticleId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Vote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.UserId })
                .ForeignKey("dbo.Article", t => t.ArticleId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.ArticleId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CommentId = c.Int(),
                        UserId = c.Int(nullable: false),
                        ArticleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.Article", t => t.ArticleId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId)
                .Index(t => t.ArticleId);
            
            CreateTable(
                "dbo.CommentVote",
                c => new
                    {
                        CommentId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Vote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.CommentId, t.UserId })
                .ForeignKey("dbo.Comment", t => t.CommentId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.CommentId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Tag",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 250, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Question",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        UserId = c.Int(nullable: false),
                        ViewsCount = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        LevelId = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Level", t => t.LevelId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LevelName = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.QuestionFollow",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.UserId })
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.QuestionVote",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        Vote = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.UserId })
                .ForeignKey("dbo.Question", t => t.QuestionId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.QuestionId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TagFollow",
                c => new
                    {
                        TagId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TagId, t.UserId })
                .ForeignKey("dbo.Tag", t => t.TagId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.TagId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        FromWhoId = c.Int(nullable: false),
                        ToWhoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.FromWhoId)
                .ForeignKey("dbo.User", t => t.ToWhoId)
                .Index(t => t.FromWhoId)
                .Index(t => t.ToWhoId);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false, maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rosette",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RosetteName = c.String(nullable: false, maxLength: 100, unicode: false),
                        Description = c.String(nullable: false, maxLength: 1000, unicode: false),
                        Image = c.Binary(nullable: false, storeType: "image"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserFollow",
                c => new
                    {
                        WhoFollowedId = c.Int(nullable: false),
                        FollowedFromWhoId = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.WhoFollowedId, t.FollowedFromWhoId })
                .ForeignKey("dbo.User", t => t.WhoFollowedId)
                .ForeignKey("dbo.User", t => t.FollowedFromWhoId)
                .Index(t => t.WhoFollowedId)
                .Index(t => t.FollowedFromWhoId);
            
            CreateTable(
                "dbo.Question_Tag",
                c => new
                    {
                        QuestionId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.QuestionId, t.TagId })
                .ForeignKey("dbo.Question", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Article_Tag",
                c => new
                    {
                        ArticleId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArticleId, t.TagId })
                .ForeignKey("dbo.Article", t => t.ArticleId, cascadeDelete: true)
                .ForeignKey("dbo.Tag", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ArticleId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.User_Rosette",
                c => new
                    {
                        RosetteId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RosetteId, t.UserId })
                .ForeignKey("dbo.Rosette", t => t.RosetteId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.RosetteId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AnswerVote", "AnswerId", "dbo.Answer");
            DropForeignKey("dbo.UserFollow", "FollowedFromWhoId", "dbo.User");
            DropForeignKey("dbo.UserFollow", "WhoFollowedId", "dbo.User");
            DropForeignKey("dbo.TagFollow", "UserId", "dbo.User");
            DropForeignKey("dbo.User_Rosette", "UserId", "dbo.User");
            DropForeignKey("dbo.User_Rosette", "RosetteId", "dbo.Rosette");
            DropForeignKey("dbo.User", "RoleId", "dbo.Role");
            DropForeignKey("dbo.QuestionVote", "UserId", "dbo.User");
            DropForeignKey("dbo.Question", "UserId", "dbo.User");
            DropForeignKey("dbo.QuestionFollow", "UserId", "dbo.User");
            DropForeignKey("dbo.Notification", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "ToWhoId", "dbo.User");
            DropForeignKey("dbo.Message", "FromWhoId", "dbo.User");
            DropForeignKey("dbo.CommentVote", "UserId", "dbo.User");
            DropForeignKey("dbo.Comment", "UserId", "dbo.User");
            DropForeignKey("dbo.ArticleVote", "UserId", "dbo.User");
            DropForeignKey("dbo.Article", "UserId", "dbo.User");
            DropForeignKey("dbo.ArticleFollow", "UserId", "dbo.User");
            DropForeignKey("dbo.Article_Tag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Article_Tag", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.TagFollow", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Question_Tag", "TagId", "dbo.Tag");
            DropForeignKey("dbo.Question_Tag", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.QuestionVote", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.QuestionFollow", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Question", "LevelId", "dbo.Level");
            DropForeignKey("dbo.Answer", "QuestionId", "dbo.Question");
            DropForeignKey("dbo.Comment", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.CommentVote", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.Comment", "CommentId", "dbo.Comment");
            DropForeignKey("dbo.ArticleVote", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.ArticleFollow", "ArticleId", "dbo.Article");
            DropForeignKey("dbo.AnswerVote", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "UserId", "dbo.User");
            DropForeignKey("dbo.Answer", "AnswerId", "dbo.Answer");
            DropIndex("dbo.User_Rosette", new[] { "UserId" });
            DropIndex("dbo.User_Rosette", new[] { "RosetteId" });
            DropIndex("dbo.Article_Tag", new[] { "TagId" });
            DropIndex("dbo.Article_Tag", new[] { "ArticleId" });
            DropIndex("dbo.Question_Tag", new[] { "TagId" });
            DropIndex("dbo.Question_Tag", new[] { "QuestionId" });
            DropIndex("dbo.UserFollow", new[] { "FollowedFromWhoId" });
            DropIndex("dbo.UserFollow", new[] { "WhoFollowedId" });
            DropIndex("dbo.Notification", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "ToWhoId" });
            DropIndex("dbo.Message", new[] { "FromWhoId" });
            DropIndex("dbo.TagFollow", new[] { "UserId" });
            DropIndex("dbo.TagFollow", new[] { "TagId" });
            DropIndex("dbo.QuestionVote", new[] { "UserId" });
            DropIndex("dbo.QuestionVote", new[] { "QuestionId" });
            DropIndex("dbo.QuestionFollow", new[] { "UserId" });
            DropIndex("dbo.QuestionFollow", new[] { "QuestionId" });
            DropIndex("dbo.Question", new[] { "LevelId" });
            DropIndex("dbo.Question", new[] { "UserId" });
            DropIndex("dbo.CommentVote", new[] { "UserId" });
            DropIndex("dbo.CommentVote", new[] { "CommentId" });
            DropIndex("dbo.Comment", new[] { "ArticleId" });
            DropIndex("dbo.Comment", new[] { "UserId" });
            DropIndex("dbo.Comment", new[] { "CommentId" });
            DropIndex("dbo.ArticleVote", new[] { "UserId" });
            DropIndex("dbo.ArticleVote", new[] { "ArticleId" });
            DropIndex("dbo.Article", new[] { "UserId" });
            DropIndex("dbo.ArticleFollow", new[] { "UserId" });
            DropIndex("dbo.ArticleFollow", new[] { "ArticleId" });
            DropIndex("dbo.User", new[] { "RoleId" });
            DropIndex("dbo.AnswerVote", new[] { "UserId" });
            DropIndex("dbo.AnswerVote", new[] { "AnswerId" });
            DropIndex("dbo.Answer", new[] { "QuestionId" });
            DropIndex("dbo.Answer", new[] { "UserId" });
            DropIndex("dbo.Answer", new[] { "AnswerId" });
            DropTable("dbo.User_Rosette");
            DropTable("dbo.Article_Tag");
            DropTable("dbo.Question_Tag");
            DropTable("dbo.UserFollow");
            DropTable("dbo.Rosette");
            DropTable("dbo.Role");
            DropTable("dbo.Notification");
            DropTable("dbo.Message");
            DropTable("dbo.TagFollow");
            DropTable("dbo.QuestionVote");
            DropTable("dbo.QuestionFollow");
            DropTable("dbo.Level");
            DropTable("dbo.Question");
            DropTable("dbo.Tag");
            DropTable("dbo.CommentVote");
            DropTable("dbo.Comment");
            DropTable("dbo.ArticleVote");
            DropTable("dbo.Article");
            DropTable("dbo.ArticleFollow");
            DropTable("dbo.User");
            DropTable("dbo.AnswerVote");
            DropTable("dbo.Answer");
        }
    }
}
