using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetReu.Models
{
    public class ReuContext : DbContext
    {
        public ReuContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Flight> Flight { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Billet> Billets { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Stock> Stocks { get; set; }

    }
}
