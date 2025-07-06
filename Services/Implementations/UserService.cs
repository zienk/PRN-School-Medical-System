using BusinessObjects.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public void AddUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Password không được để trống.");

            if (_userRepository.IsUserExists(user.Username))
                throw new InvalidOperationException("Tên người dùng đã tồn tại.");

            user.UserId = Guid.NewGuid(); 
            user.IsActive = true; 
            user.IsFirstLogin ??= true;
            
            _userRepository.AddUser(user);
        }

        public void DeleteUser(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId không hợp lệ.");

            var user = _userRepository.GetUserById(userId);
            if (user == null)
                throw new InvalidOperationException("Không tìm thấy người dùng để xóa!!");

            _userRepository.DeleteUser(userId);
        }

        public List<User> GetAllUsers()
            => _userRepository.GetAllUsers();

        public User? GetUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username và password không được để trống.");

            return _userRepository.GetUser(username, password);
        }
            
        public User? GetUserById(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId không được để trống.");
            
            return _userRepository.GetUserById(userId);
        }
            

        public List<User> SearchUsers(string searchText)
            => _userRepository.SearchUsers(searchText ?? "");


        public void UpdateUser(User user)
        {
            if (user == null || user.UserId == Guid.Empty)
                throw new ArgumentException("Thông tin người dùng không hợp lệ.");

            var existing = _userRepository.GetUserById(user.UserId);
            if (existing == null)
                throw new InvalidOperationException("Không tìm thấy người dùng để cập nhật!!");

            existing.FullName = user.FullName;
            existing.Phone = user.Phone;
            existing.Email = user.Email;
            existing.Address = user.Address;
            existing.RoleId = user.RoleId;

            _userRepository.UpdateUser(existing);
        }
    }
}
