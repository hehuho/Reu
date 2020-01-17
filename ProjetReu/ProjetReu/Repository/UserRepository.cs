using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetReu.Models;

namespace ProjetReu.Repository
{
    public class UserRepository : IUserRepository
    {
        private ReuContext _reuContext { get; set; }

        public UserRepository(ReuContext reuContext)
        {
            _reuContext = reuContext;
        }

        public List<User> getListUsers()
        {
            return _reuContext.Users.ToList();
        }

        public void addUser(User user)
        {
            _reuContext.Add(user);
            _reuContext.SaveChanges(); ;
        }
    }
}
