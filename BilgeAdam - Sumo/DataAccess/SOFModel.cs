namespace BilgeAdam___Sumo.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SOFModel : DbContext
    {
        public SOFModel()
            : base("name=SOFModel")
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AnswerVote> AnswerVotes { get; set; }
        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<ArticleFollow> ArticleFollows { get; set; }
        public virtual DbSet<ArticleVote> ArticleVotes { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<CommentVote> CommentVotes { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<QuestionFollow> QuestionFollows { get; set; }
        public virtual DbSet<QuestionVote> QuestionVotes { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Rosette> Rosettes { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagFollow> TagFollows { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFollow> UserFollows { get; set; }

        public virtual DbSet<UserRosette> UserRosettes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.Answer1)
                .WithOptional(e => e.Answer2)
                .HasForeignKey(e => e.AnswerId);

            modelBuilder.Entity<Answer>()
                .HasMany(e => e.AnswerVotes)
                .WithRequired(e => e.Answer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRosette>()
                .HasRequired(e => e.Rosette)
                .WithMany(e => e.UserRosettes);

            modelBuilder.Entity<UserRosette>()
                .HasRequired(e => e.User)
                .WithMany(e => e.UserRosettes);

            modelBuilder.Entity<Article>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.ArticleFollows)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.ArticleVotes)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Articles)
                .Map(m => m.ToTable("Article_Tag").MapLeftKey("ArticleId").MapRightKey("TagId"));

            modelBuilder.Entity<Comment>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.Comment1)
                .WithOptional(e => e.Comment2)
                .HasForeignKey(e => e.CommentId);

            modelBuilder.Entity<Comment>()
                .HasMany(e => e.CommentVotes)
                .WithRequired(e => e.Comment)
                .WillCascadeOnDelete(false);

           

            modelBuilder.Entity<Level>()
                .Property(e => e.LevelName)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Notification>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Title)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionFollows)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.QuestionVotes)
                .WithRequired(e => e.Question)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Questions)
                .Map(m => m.ToTable("Question_Tag").MapLeftKey("QuestionId").MapRightKey("TagId"));

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Rosette>()
                .Property(e => e.RosetteName)
                .IsUnicode(false);

            modelBuilder.Entity<Rosette>()
                .Property(e => e.Description)
                .IsUnicode(false);

            //modelBuilder.Entity<Rosette>()
            //    .HasMany(e => e.Users)
            //    .WithMany(e => e.Rosettes)
            //    .Map(m => m.ToTable("User_Rosette").MapLeftKey("RosetteId").MapRightKey("UserId"));

            modelBuilder.Entity<Tag>()
                .Property(e => e.TagName)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .HasMany(e => e.TagFollows)
                .WithRequired(e => e.Tag)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Mail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Region)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.About)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Site)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Language)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.AnswerVotes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ArticleFollows)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ArticleVotes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

         

            modelBuilder.Entity<User>()
                .HasMany(e => e.CommentVotes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.FromWhoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Messages1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.ToWhoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Questions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.QuestionFollows)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.QuestionVotes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.TagFollows)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserFollows)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.WhoFollowedId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserFollows1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.FollowedFromWhoId)
                .WillCascadeOnDelete(false);
        }
    }
}
