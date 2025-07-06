using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Services.Interfaces;
using BusinessObjects.Entities;
using Services.Implementations;
namespace WPF.SchoolMedicalManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {

        private readonly IUserService _userService;

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Password.Trim();

            var user = await _userService.GetUserAsync(username, password);

            if (user != null)
            {
                if (user.Role.RoleId == 1)
                {
                    ///
                    MessageBox.Show($"Welcome {user.FullName} with {user.Role.RoleName}", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (user.Role.RoleId == 2)
                {
                    ///
                    MessageBox.Show($"Welcome {user.FullName} with {user.Role.RoleName} ", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                if (user.Role.RoleId == 3)
                {
                    ///
                    MessageBox.Show($"Welcome {user.FullName} with {user.Role.RoleName}", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
