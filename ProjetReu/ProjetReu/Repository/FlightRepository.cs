using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetReu.Models;

namespace ProjetReu.Repository
{
    public class FlightRepository : IFlightRepository
    {
        private ReuContext _reuContext { get; set; }

        public FlightRepository(ReuContext reuContext)
        {
            _reuContext = reuContext;
        }

        public List<Flight> getFlightList()
        {
            return _reuContext.Flight.ToList();
        }
    }
}
