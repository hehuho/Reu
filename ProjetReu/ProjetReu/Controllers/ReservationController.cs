using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using ProjetReu.Models;
using ProjetReu.Repository;

namespace ProjetReu.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IStockRepository _stockRepository;

        public ReservationController(IUserRepository userRepository,
                                     IStockRepository stockRepository)
        {
            _userRepository = userRepository;
            _stockRepository = stockRepository;
        }

        // POST api/reservation/submit
        [HttpPost("submit")]
        public ActionResult Post([FromBody]JObject body)
        {
            if (body.HasValues
                && body.ContainsKey("ClasseId")
                && body.ContainsKey("NomUtilisateur")
                && body.ContainsKey("PrénomUtilisateur")
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

                if(int.TryParse(classeId, out classeIdValue)
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

                    if(userToCheck == null)
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

                    if(stockToCheck.NbStock > 0)
                    {
                        stockToCheck.NbStock--;
                        _stockRepository.UpdateStock(stockToCheck);


                    }
                    else
                        return BadRequest("Il n'y a plus de place disponible pour ce vol !!!");



                    return Ok();
                }
                else
                    return BadRequest("Une des valeurs rentrées n'est pas au bon format !");

            }
            else
                return BadRequest("Le JSON envoyé n'est pas formaté correctement !");
            
        }
    }
}