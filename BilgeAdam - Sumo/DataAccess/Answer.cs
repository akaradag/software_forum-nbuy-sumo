namespace BilgeAdam___Sumo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Answer")]
    public partial class Answer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Answer()
        {
            Answer1 = new HashSet<Answer>();
            AnswerVotes = new HashSet<AnswerVote>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(8000)]
        public string Content { get; set; }

        public int? AnswerId { get; set; }

        public int UserId { get; set; }

        public int QuestionId { get; set; }

        public bool Accepted { get; set; }

        public DateTime CreateTime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answer> Answer1 { get; set; }

        public virtual Answer Answer2 { get; set; }

        public virtual Question Question { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AnswerVote> AnswerVotes { get; set; }
    }
}
