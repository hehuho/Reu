using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetReu.Models;

namespace ProjetReu.Repository
{
    public class BilletRepository : IBilletRepository
    {
        private ReuContext _reuContext { get; set; }

        public BilletRepository(ReuContext reuContext)
        {
            _reuContext = reuContext;
        }

        public void addBillet(Billet billet)
        {
            _reuContext.Add(billet);
        }
    }
}
