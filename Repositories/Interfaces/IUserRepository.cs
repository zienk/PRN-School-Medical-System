using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserAsync(string username, string password);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> SearchUsersAsync(string searchText);
        Task<User?> GetUserByIdAsync(Guid userId);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(Guid userId);
        Task<bool> IsUserExistsAsync(string username);    

    }
}
