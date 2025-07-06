using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.ViewModels
{
    public partial class LoginWindowViewModels : ObservableObject
    {
        private readonly IUserService _userService;

        public LoginWindowViewModels()
        {
            _userService = new UserService();
            ErrorMessage = string.Empty;
        }

        [ObservableProperty]
        private string userId;

        [ObservableProperty]
        private string username;

        [ObservableProperty]
        private string password;

        [ObservableProperty]
        private int roleid;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private bool isLoginSuccessful;

        [RelayCommand]
        private async Task LoginAsync()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Tên đăng nhập và mật khẩu là bắt buộc.";
                return;
            }

            var userTask = _userService.GetUserAsync(Username, Password);
            var user = await userTask; // await the task to get the user

            if (user != null)
            {
                Roleid = user.RoleId;
                ErrorMessage = "Đăng nhập thành công!";
                // Điều hướng đến cửa sổ chính hoặc thực hiện các hành động khác
                IsLoginSuccessful = true;
            }
            else
            {
                ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
                IsLoginSuccessful = false;
            }
        }
    }
}
