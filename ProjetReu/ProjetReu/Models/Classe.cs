﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetReu.Models
{
    public class Classe
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ClasseId { get; set; }

        [ForeignKey("FK_FlightId")]
        public int FlightId { get; set; }

        public string Name { get; set; }

        public int NbSiege { get; set; }

        public decimal Price { get; set; }

    }
}
