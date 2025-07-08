using DataAccessLayer;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly PrnEduHealthContext _context;

        public UserRepository()
        {
            _context = new PrnEduHealthContext();
        }

        public void AddUser(User user)
        {
            _context.Users.AddAsync(user);
            _context.SaveChangesAsync();
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetUserById(userId);

            if (user != null)
            {
                user.IsActive = false;
                _context.Users.Update(user);
                _context.SaveChanges();
            }
        }

        public List<User> GetAllUsers()
            => _context.Users
                .Include(u => u.Role)
                .AsNoTracking()
                .Where(u => u.IsActive == true)
                .ToList();

        public User? GetUser(string username, string password)
            => _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Username == username && u.Password == password && u.IsActive == true);
        

        public User? GetUserById(Guid userId)
            => _context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.UserId == userId && u.IsActive == true);

        public List<User> SearchUsers(string searchText)
        {
            searchText = searchText.ToLower();

            return _context.Users
                .Include(u => u.Role)
                .Where(u => (u.Username.ToLower().Contains(searchText) ||
                             u.Email.ToLower().Contains(searchText) ||
                             u.Phone.Contains(searchText) ||
                             u.Address.ToLower().Contains(searchText) ||
                             u.FullName.ToLower().Contains(searchText)) &&
                             u.IsActive == true)
                .ToList();
        }
            

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public bool IsUserExists(string username)
            => _context.Users.Any(u => u.Username == username && u.IsActive == true);

    }
}
