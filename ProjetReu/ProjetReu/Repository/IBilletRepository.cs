using ProjetReu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetReu.Repository
{
    public interface IBilletRepository
    {
        void addBillet(Billet billet);
    }
}
