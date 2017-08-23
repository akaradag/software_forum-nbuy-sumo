namespace BilgeAdam___Sumo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Answers = new HashSet<Answer>();
            AnswerVotes = new HashSet<AnswerVote>();
            Articles = new HashSet<Article>();
            ArticleFollows = new HashSet<ArticleFollow>();
            ArticleVotes = new HashSet<ArticleVote>();
            Comments = new HashSet<Comment>();
            CommentVotes = new HashSet<CommentVote>();
            Messages = new HashSet<Message>();
            Messages1 = new HashSet<Message>();
            Notifications = new HashSet<Notification>();
            Questions = new HashSet<Question>();
            QuestionFollows = new HashSet<QuestionFollow>();
            QuestionVotes = new HashSet<QuestionVote>();
            TagFollows = new HashSet<TagFollow>();
            UserFollows = new HashSet<UserFollow>();
            UserFollows1 = new HashSet<UserFollow>();
            UserRosettes = new HashSet<UserRosette>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        // Tam ismi kullanmak için :)
        public string GetFullName
        {
           get
            {
                string fullName = String.Format("{0} {1}", FirstName, LastName);
                return fullName;
            }
        }

        [Required]
        [StringLength(100)]
        public string Mail { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(8000)]
        public string About { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }

        [StringLength(100)]
        public string Company { get; set; }

        [StringLength(50)]
        public string Site { get; set; }

        [StringLength(50)]
        public string Language { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public int RoleId { get; set; }

        public int RepPoint { get; set; }

        [Column(TypeName = "date")]
        public DateTime AccountCreateDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Article> Articles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleFollow> ArticleFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ArticleVote> ArticleVotes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentVote> CommentVotes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Notification> Notifications { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Question> Questions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionFollow> QuestionFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QuestionVote> QuestionVotes { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TagFollow> TagFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFollow> UserFollows { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserFollow> UserFollows1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRosette> UserRosettes { get; set; }
    }
}
