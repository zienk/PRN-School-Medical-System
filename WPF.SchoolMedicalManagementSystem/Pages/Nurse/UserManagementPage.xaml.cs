using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class UserManagementPage : Page
    {
        public UserManagementPage()
        {
            InitializeComponent();
            LoadUsers(); // Hàm này sẽ tải dữ liệu người dùng
        }

        private void LoadUsers()
        {
            // Logic để tải danh sách người dùng từ database và gán vào dgUsers.ItemsSource
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            // Logic để mở cửa sổ/dialog thêm người dùng mới
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            // Logic để sửa thông tin người dùng được chọn
        }

        private void ToggleUserStatus_Click(object sender, RoutedEventArgs e)
        {
            // Logic để vô hiệu hóa/kích hoạt tài khoản người dùng được chọn
        }

        private void SearchUser_Click(object sender, RoutedEventArgs e)
        {
            // Logic tìm kiếm người dùng
        }

        private void DgUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Xử lý khi chọn một hàng trong DataGrid
        }
    }
}