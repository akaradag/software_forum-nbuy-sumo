namespace BilgeAdam___Sumo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(8000)]
        public string Content { get; set; }

        public int FromWhoId { get; set; }

        public int ToWhoId { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
