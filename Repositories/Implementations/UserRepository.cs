    using DataAccessLayer;
using BusinessObjects.Entities;
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

        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            var user = await GetUserByIdAsync(userId);

            if (user != null)
            {
                user.IsActive = false;
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        public Task<List<User>> GetAllUsersAsync()
            => _context.Users
                .Include(u => u.Role)
                .Where(u => u.IsActive == true)
                .ToListAsync();

        public Task<User?> GetUserAsync(string username, string password)
            => _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.Password == password && u.IsActive == true);
        

        public Task<User?> GetUserByIdAsync(Guid userId)
            => _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId && u.IsActive == true);

        public Task<List<User>> SearchUsersAsync(string searchText)
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
                .ToListAsync();
        }
            

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsUserExistsAsync(string username)
            => _context.Users.AnyAsync(u => u.Username == username && u.IsActive == true);

    }
}
