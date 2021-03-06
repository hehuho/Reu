﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetReu.Models
{
    public class Billet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long BilletId { get; set; }

        [ForeignKey("FK_UserId")]
        public int UserId { get; set; }

        [ForeignKey("FK_ClasseId")]
        public int ClasseId { get; set; }

        public DateTime ReservationDate { get; set; }
    }
}
