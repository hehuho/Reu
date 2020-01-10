using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Reu.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long FlightId { get; set; }
        public string Name { get; set; }
        public string Classe { get; set; }
        public int NbSiege { get; set; }
        public decimal Price { get; set; }
        public DateTime DateFlight { get; set; }
    }
}
