using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Services.Interfaces
{
    public interface IUserService
    {
        User? GetUser(string username, string password);
        List<User> GetAllUsers();
        List<User> SearchUsers(string searchText);
        User? GetUserById(Guid userId);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(Guid userId);

        //Lấy danh sách người dùng theo role
        List<User> GetUsersByRole(int roleId);

        // Kiểm tra mật khẩu của người dùng
        bool ValidatePassword(string username, string password);
        
        // Lấy user chỉ dựa trên username, bao gồm cả tài khoản bị vô hiệu hóa
        User? GetUserByUsername(string username);
    }
}
