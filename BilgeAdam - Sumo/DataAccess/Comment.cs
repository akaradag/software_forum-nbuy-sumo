namespace BilgeAdam___Sumo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            Comment1 = new HashSet<Comment>();
            CommentVotes = new HashSet<CommentVote>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(8000)]
        public string Content { get; set; }

        public int? CommentId { get; set; }

        public int UserId { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment1 { get; set; }

        public virtual Comment Comment2 { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CommentVote> CommentVotes { get; set; }
    }
}
