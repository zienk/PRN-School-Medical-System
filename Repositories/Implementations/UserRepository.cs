using DataAccessLayer;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly PrnEduHealthContext _context;

        public UserRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public User? GetUser(string username, string password)
        {
            return _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive == true);
        }


    }
}
