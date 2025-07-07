using Services.Implementations;
using Services.Interfaces;
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

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private readonly IUserService _userService;

        public UserManagement()
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUserData();
        }

        private void LoadUserData()
        {
            dgUsers.ItemsSource = _userService.GetAllUsers();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                dgUsers.ItemsSource = _userService.SearchUsers(searchText);
            }
            else
            {
                LoadUserData();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ResetPasswordButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBackToDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

      
    }
}
