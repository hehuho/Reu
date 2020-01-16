using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetReu.Models;

namespace ProjetReu.Repository
{
    public class ClasseRepository : IClasseRepository
    {
        private ReuContext _reuContext { get; set; }

        public ClasseRepository(ReuContext reuContext)
        {
            _reuContext = reuContext;
        }

        public List<Classe> getClasseList()
        {
            return _reuContext.Classes.ToList();
        }
    }
}
