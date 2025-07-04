using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAccessLayer.Entities;
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
        private void Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Tên đăng nhập và mật khẩu là bắt buộc.";
                return;
            }

            var user = _userService.GetUser(Username, Password);
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
