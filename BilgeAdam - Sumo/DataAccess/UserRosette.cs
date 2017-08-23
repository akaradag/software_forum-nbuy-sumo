using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BilgeAdam___Sumo.DataAccess
{
    [Table("UserRosette")]
    public class UserRosette
    {

        //public int Id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RosetteId { get; set; }

        public DateTime Date { get; set; }

        public virtual User User { get; set; }

        public virtual Rosette Rosette { get; set; }

    }
}