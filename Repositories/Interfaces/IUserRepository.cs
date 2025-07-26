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
        void AddUser(User user); // Hàm này nên trả về object User
        void UpdateUser(User user); // Hàm này nên trả về object User
        void DeleteUser(Guid userId); // Hàm này nên trả về bool
        
        // Hàm check duplicate username này hổ trợ hàm tạo mới user
        bool IsUserExists(string username);

        //Lấy danh sách người dùng theo role
        List<User> GetUsersByRole(int roleId);
        
        // Lấy user theo username (bỏ qua mật khẩu và trạng thái)
        User? GetUserByUsername(string username);
    }
}
