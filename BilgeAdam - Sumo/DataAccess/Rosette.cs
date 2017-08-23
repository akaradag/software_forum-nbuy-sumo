namespace BilgeAdam___Sumo.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rosette")]
    public partial class Rosette
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Rosette()
        {
            UserRosettes = new HashSet<UserRosette>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string RosetteName { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Column(TypeName = "image")]
        [Required]
        public byte[] Image { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRosette> UserRosettes { get; set; }
    }
}
