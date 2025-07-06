using BusinessObjects.Entities;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public async Task AddUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentException("Password không được để trống.");

            if (await _userRepository.IsUserExistsAsync(user.Username))
                throw new InvalidOperationException("Tên người dùng đã tồn tại.");

            user.UserId = Guid.NewGuid(); 
            user.IsActive = true; 
            user.IsFirstLogin ??= true;
            
            await _userRepository.AddUserAsync(user);
        }

        public async Task DeleteUserAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId không hợp lệ.");

            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("Không tìm thấy người dùng để xóa!!");

            await _userRepository.DeleteUserAsync(userId);
        }

        public Task<List<User>> GetAllUsersAsync()
            => _userRepository.GetAllUsersAsync();

        public Task<User?> GetUserAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Username và password không được để trống.");

            return _userRepository.GetUserAsync(username, password);
        }
            
        public Task<User?> GetUserByIdAsync(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId không được để trống.");
            
            return _userRepository.GetUserByIdAsync(userId);
        }
            

        public Task<List<User>> SearchUsersAsync(string searchText)
            => _userRepository.SearchUsersAsync(searchText ?? "");

        public async Task UpdateUserAsync(User user)
        {
            if (user == null || user.UserId == Guid.Empty)
                throw new ArgumentException("Thông tin người dùng không hợp lệ.");

            var existing = await _userRepository.GetUserByIdAsync(user.UserId);
            if (existing == null)
                throw new InvalidOperationException("Không tìm thấy người dùng để cập nhật!!");

            existing.FullName = user.FullName;
            existing.Phone = user.Phone;
            existing.Email = user.Email;
            existing.Address = user.Address;
            existing.RoleId = user.RoleId;

            await _userRepository.UpdateUserAsync(existing);
        }
    }
}
