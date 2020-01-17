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
    public class ReservationController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IClasseRepository _classeRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBilletRepository _billetRepository;

        public ReservationController(IFlightRepository flightRepository, 
                                 IClasseRepository classeRepository,
                                 IStockRepository stockRepository,
                                 IUserRepository userRepository,
                                 IBilletRepository billetRepository)
        {
            _flightRepository = flightRepository;
            _classeRepository = classeRepository;
            _stockRepository = stockRepository;
            _userRepository = userRepository;
            _billetRepository = billetRepository;
        }

        // GET api/display/GetAll
        [HttpGet("get")]
        public ActionResult<List<Flight>> Get()
        {
            return Ok(_flightRepository.getFlightList());
        }

        // POST api/reservation/returnList
        [HttpPost("returnList")]
        public ActionResult<List<ViewFlightClasse>> PostDisplay([FromBody]JObject body)
        {
            if (body.HasValues
                && body.ContainsKey("Date"))
            {
                string date = body.GetValue("Date").ToString();
                DateTime dateValue;
                if (DateTime.TryParse(date, out dateValue))
                {
                    if(dateValue >= DateTime.Now.Date)
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

                                    viewFlightClasse.ClasseList.Add(classeListItem);
                                }

                                if (stock != null && stock.NbStock > 0)
                                    viewFlightClasse.ClasseList.Add(classeListItem);


                            }

                            viewFlightClasseList.Add(viewFlightClasse);
                        }

                        return Ok(viewFlightClasseList);
                    }
                    else
                        return BadRequest(new { message = "La date saisie est antérieure à la date du jour !" });


                }
                else
                    return BadRequest(new { message = "La date n'est pas correctement saisie !" });

            }
            else
                return BadRequest(new { message = "Le Json dans le body ne contient pas de clé Date !" });
        }

        // POST api/reservation/submit
        [HttpPost("submit")]
        public ActionResult PostSubmit([FromBody]JObject body)
        {
            if (body.HasValues
                && body.ContainsKey("ClasseId")
                && body.ContainsKey("NomUtilisateur")
                && body.ContainsKey("PrenomUtilisateur")
                && body.ContainsKey("Telephone")
                && body.ContainsKey("Address")
                && body.ContainsKey("Date"))
            {
                string classeId = body.GetValue("ClasseId").ToString();
                string nomUtilisateur = body.GetValue("NomUtilisateur").ToString();
                string prenomUtilisateur = body.GetValue("PrenomUtilisateur").ToString();
                string telephone = body.GetValue("Telephone").ToString();
                string address = body.GetValue("Address").ToString();
                string date = body.GetValue("Date").ToString();

                int classeIdValue;
                DateTime dateValue;
                int userId;

                if (int.TryParse(classeId, out classeIdValue)
                   && !string.IsNullOrEmpty(nomUtilisateur)
                   && !string.IsNullOrEmpty(prenomUtilisateur)
                   && !string.IsNullOrEmpty(telephone)
                   && !string.IsNullOrEmpty(address)
                   && DateTime.TryParse(date, out dateValue))
                {
                    User userToCheck = _userRepository.getListUsers()
                                                      .Where(user => user.FirstName == prenomUtilisateur
                                                                  && user.LastName == nomUtilisateur
                                                                  && user.Address == address
                                                                  && user.NumTel == telephone)
                                                      .SingleOrDefault();

                    if (userToCheck == null)
                    {
                        User userToAdd = new User();
                        userToAdd.FirstName = prenomUtilisateur;
                        userToAdd.LastName = nomUtilisateur;
                        userToAdd.Address = address;
                        userToAdd.NumTel = telephone;
                        userToAdd.CreationDate = dateValue;

                        _userRepository.addUser(userToAdd);

                        userId = Convert.ToInt32(_userRepository.getListUsers()
                                                .Where(user => user.FirstName == prenomUtilisateur
                                                                  && user.LastName == nomUtilisateur
                                                                  && user.Address == address
                                                                  && user.NumTel == telephone)
                                                .Select(user => user.UserId)
                                                .SingleOrDefault());

                    }
                    else
                    {
                        userId = Convert.ToInt32(userToCheck.UserId);
                    }

                    Stock stockToCheck = _stockRepository.getListStock()
                                                         .Where(st => st.ClasseId == classeIdValue)
                                                         .SingleOrDefault();

                    if (stockToCheck.NbStock > 0)
                    {
                        stockToCheck.NbStock--;
                        _stockRepository.UpdateStock(stockToCheck);

                        Billet billetToAdd = new Billet();
                        billetToAdd.ClasseId = classeIdValue;
                        billetToAdd.ReservationDate = dateValue;
                        billetToAdd.UserId = userId;

                        _billetRepository.addBillet(billetToAdd);

                        return Ok(new { message = "Réservation effectuée avec succès !!!!" });
                    }
                    else
                        return BadRequest(new { message = "Il n'y a plus de place disponible pour ce vol !!!" });

                }
                else
                    return BadRequest(new { message = "Une des valeurs rentrées n'est pas au bon format !" });

            }
            else
                return BadRequest(new { message = "Le JSON envoyé n'est pas formaté correctement !" });

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
