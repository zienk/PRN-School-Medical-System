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
using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        private IUserService _usersive;
        public UserManagement()
        {
            InitializeComponent();
            LoadData();
        }


        public void LoadData()
        {
            _usersive = new UserService();
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = _usersive.GetAllUsers();
        }
        private void btnBackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboard dashborhd = new ManagerDashboard();
            dashborhd.Show();
            this.Close();
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement manage users logic
        }

        private void btnManageStudents_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement manage students logic
        }

        private void btnManageHealthRecords_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement manage health records logic
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // TODO: Implement search logic on key up
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement search logic on button click
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement add new user logic
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement edit user logic
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedUser = dgUsers.SelectedItem as User;
            if (selectedUser != null)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa người dùng '{selectedUser.FullName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _usersive.DeleteUser(selectedUser.UserId);  
                        LoadData(); 
                        MessageBox.Show("Xóa người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi xóa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng cần xóa!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

    }
}
