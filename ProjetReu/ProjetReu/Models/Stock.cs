using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetReu.Models
{
    public class Stock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StockId { get; set; }

        [ForeignKey("FK_ClasseId")]
        public int ClasseId { get; set; }

        public int NbStock { get; set; }
    }
}
