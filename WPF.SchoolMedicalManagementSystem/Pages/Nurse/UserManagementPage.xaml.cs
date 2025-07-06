using Services.Implementations;
using Services.Interfaces;
using System.Net.WebSockets;
using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class UserManagementPage : Page
    {
        private readonly IUserService _userService;

        public UserManagementPage()
        {
            InitializeComponent();

            _userService = new UserService();

            LoadUsers(); 
        }

        private async void LoadUsers()
        {
            try
            {
                dgUsers.ItemsSource = await _userService.GetAllUsersAsync();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var userDetailWindow = new Views.UserDetailWindow(_userService);
            if (userDetailWindow.ShowDialog() == true)
            {
                LoadUsers();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is BusinessObjects.Entities.User selectedUser)
            {
                var userDetailWindow = new Views.UserDetailWindow(_userService, selectedUser);
                if (userDetailWindow.ShowDialog() == true)
                {
                    LoadUsers();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void DeactivateUser_Click(object sender, RoutedEventArgs e)
        {
            if (dgUsers.SelectedItem is BusinessObjects.Entities.User selectedUser)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show($"Bạn có chắc chắn muốn vô hiệu hóa tài khoản '{selectedUser.Username}' không?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        await _userService.DeleteUserAsync(selectedUser.UserId);
                        MessageBox.Show("Vô hiệu hóa người dùng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadUsers(); // Refresh the DataGrid
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi vô hiệu hóa người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một người dùng để vô hiệu hóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Xử lý khi chọn một hàng trong DataGrid
        }

        private async void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchText = txtSearchUser.Text.Trim();

            var result = await _userService.SearchUsersAsync(searchText);
            
            if (result == null || result.Count == 0)
            {
                MessageBox.Show("Không tìm thấy người dùng nào với từ khóa tìm kiếm này.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadUsers();
                return;
            }

            dgUsers.ItemsSource = result;
        }
    }
}
