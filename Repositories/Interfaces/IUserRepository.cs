using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Repositories.Interfaces
{
    public interface IUserRepository
    {
        User? GetUser(string username, string password);
        List<User> GetAllUsers();
        List<User> SearchUsers(string searchText);
        User? GetUserById(Guid userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid userId);
        bool IsUserExists(string username);    

    }
}
