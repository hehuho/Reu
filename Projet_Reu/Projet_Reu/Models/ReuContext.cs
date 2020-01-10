using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet_Reu.Models
{
    public class ReuContext : DbContext
    {
        public ReuContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Flight> Flight { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFlightRelation> UserFlightRelations { get; set; }

    }
}
