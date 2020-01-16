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
        private readonly IStockRepository _stockRepository;

        public DisplayController(IFlightRepository flightRepository, 
                                 IClasseRepository classeRepository,
                                 IStockRepository stockRepository)
        {
            _flightRepository = flightRepository;
            _classeRepository = classeRepository;
            _stockRepository = stockRepository;
        }

        // GET api/display/GetAll
        [HttpGet("get")]
        public ActionResult<List<Flight>> Get()
        {
            return Ok(_flightRepository.getFlightList());
        }

        // POST api/display/returnList
        [HttpPost("returnList")]
        public ActionResult<List<ViewFlightClasse>> Post([FromBody]JObject body)
        {
            if (body.HasValues
                && body.ContainsKey("Date"))
            {
                string date = body.GetValue("Date").ToString();
                DateTime dateValue;
                if (DateTime.TryParse(date, out dateValue))
                {
                    List<Flight> flightList = _flightRepository.getFlightList();
                    List<ViewFlightClasse> viewFlightClasseList = new List<ViewFlightClasse>();
                    foreach (var flightListItem in flightList)
                    {
                        ViewFlightClasse viewFlightClasse = new ViewFlightClasse();
                        viewFlightClasse.FlightId = Convert.ToInt32(flightListItem.FlightId);
                        viewFlightClasse.FlightName = flightListItem.Name;
                        viewFlightClasse.ClasseList = new List<Classe>();
                        List<Classe> classeList = _classeRepository.getClasseList().Where(cl => cl.FlightId == flightListItem.FlightId).ToList();
                        foreach (var classeListItem in classeList)
                        {
                            Stock stock = _stockRepository.getListStock()
                                                          .Where(st => st.ClasseId == classeListItem.ClasseId
                                                                    && st.DateStock == dateValue)
                                                          .SingleOrDefault();

                            if (stock == null)
                            {
                                Stock stockToAdd = new Stock();
                                stockToAdd.ClasseId = Convert.ToInt32(classeListItem.ClasseId);
                                stockToAdd.NbStock = classeListItem.NbSiege;
                                stockToAdd.DateStock = dateValue;
                                _stockRepository.AddStock(stockToAdd);
                            }
                            
                            if(stock != null && stock.NbStock > 0)
                                viewFlightClasse.ClasseList.Add(classeListItem);
                                

                        }

                        viewFlightClasseList.Add(viewFlightClasse);
                    }

                    return Ok(viewFlightClasseList);
                }
                else
                    return BadRequest("La date n'est pas correctement saisie !");

            }
            else
                return BadRequest("Le Json dans le body ne contient pas de clé Date !");
        }


        #region Internal Class

        public class ViewFlightClasse
        {
            public int FlightId { get; set; }
            public string FlightName { get; set; }
            public List<Classe> ClasseList { get; set; }
        }
        
        #endregion
    }
}
