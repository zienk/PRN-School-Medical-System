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
using BusinessObjects.Entities;


namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    // Giao diện quản lý người dùng
    public partial class UserManagement : Window
    {
        private readonly IUserService _userService;

        public UserManagement()
        {
            InitializeComponent();
            _userService = new UserService();
            LoadUserData();
            UpdateRecordCount();
        }

        // Tải dữ liệu người dùng vào DataGrid
        private void LoadUserData()
        {
            dgUsers.ItemsSource = null;
            dgUsers.ItemsSource = _userService.GetAllUsers();
        }

        // Xử lý sự kiện tìm kiếm
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

        // Xử lý sự kiện thêm người dùng mới
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Mở dialog ở chế độ thêm mới
                var userDialog = new UserAddAndEdit(isEdit: false, user: null);
                userDialog.Owner = this; // Đặt window cha

                var result = userDialog.ShowDialog();

                // Nếu user đã lưu thành công, refresh lại danh sách
                if (result == true)
                {
                    LoadUserData();
                    StatusLabel.Text = "Thêm người dùng thành công!";
                    UpdateRecordCount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi mở form thêm người dùng: {ex.Message}",
                               "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Cập nhật số lượng bản ghi
        private void UpdateRecordCount()
        {
            var userCount = dgUsers.Items.Count;
            RecordCountLabel.Text = $"Tổng: {userCount} người dùng";
        }

        // Xử lý sự kiện sửa người dùng
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Lấy user được chọn từ DataGrid
                var selectedUser = dgUsers.SelectedItem as User;

                if (selectedUser == null)
                {
                    MessageBox.Show("Vui lòng chọn người dùng cần sửa!",
                                   "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Mở dialog ở chế độ chỉnh sửa với user đã chọn
                var userDialog = new UserAddAndEdit(isEdit: true, user: selectedUser);
                userDialog.Owner = this; // Đặt window cha

                var result = userDialog.ShowDialog();

                // Nếu user đã cập nhật thành công, refresh lại danh sách
                if (result == true)
                {
                    LoadUserData();
                    StatusLabel.Text = "Cập nhật người dùng thành công!";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi mở form sửa người dùng: {ex.Message}",
                               "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện xóa người dùng
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
                        _userService.DeleteUser(selectedUser.UserId);
                        LoadUserData();
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

        // Xử lý sự kiện nhấn phím trong ô tìm kiếm
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Có thể thêm tìm kiếm tự động khi gõ
        }

        // Quay lại dashboard
        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
            this.Close();
        }

    }
}
