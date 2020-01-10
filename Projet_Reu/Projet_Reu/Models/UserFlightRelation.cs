using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Reu.Models
{
    public class UserFlightRelation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserFlightRelationId { get; set; }

        [ForeignKey("FK_UserId")]
        public int UserId { get; set; }
        
        [ForeignKey("FK_FlightId")]
        public int FlightId { get; set; }
    }
}
