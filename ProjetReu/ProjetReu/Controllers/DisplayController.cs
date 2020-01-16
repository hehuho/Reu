using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProjetReu.Models;
using ProjetReu.Repository;

namespace ProjetReu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisplayController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IClasseRepository _classeRepository;

        public DisplayController(IFlightRepository flightRepository, 
                                 IClasseRepository classeRepository)
        {
            _flightRepository = flightRepository;
            _classeRepository = classeRepository;
        }

        // GET api/display/GetAll
        [HttpGet("get")]
        public ActionResult<List<Flight>> Get()
        {
            return Ok(_flightRepository.getFlightList());
        }

        // POST api/display/returnList
        [HttpPost("returnList")]
        public ActionResult Post(JObject body)
        {
            return Ok();
        }


        #region Internal Class

        internal class Display
        {

        }
        
        #endregion
    }
}
