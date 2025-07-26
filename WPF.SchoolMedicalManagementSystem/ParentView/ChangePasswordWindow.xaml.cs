using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.IO;
using System.Windows;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    public partial class ChangePasswordWindow : Window
    {
        private readonly IUserService _userService;
        private readonly User _currentUser;
        private readonly bool _isForced;

        public ChangePasswordWindow(User user, bool isForced = true)
        {
            InitializeComponent();
            _userService = new UserService();
            _currentUser = user;
            _isForced = isForced;
            
            // Kiểm tra ngay khi cửa sổ được khởi tạo
            CheckAccountStatus();

            this.Closed += ChangePasswordWindow_Closed;
        }
        
        private void CheckAccountStatus()
        {
            // Lấy thông tin user mới nhất từ database
            var user = _userService.GetUserById(_currentUser.UserId);
             
            // Nếu tài khoản đã bị vô hiệu hóa sau khi đăng nhập
            if (user == null || user.IsActive == false)
            {
                MessageBox.Show(
                    "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên để kích hoạt lại.",
                    "Tài khoản bị vô hiệu hóa",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                     
                Application.Current.Shutdown();
            }
        }

        private void ChangePasswordWindow_Closed(object sender, EventArgs e)
        {
            // Nếu là đăng nhập lần đầu và người dùng đóng cửa sổ mà không đổi mật khẩu
            // thì đóng ứng dụng
            if (_isForced && _currentUser.IsFirstLogin == true)
            {
                Application.Current.Shutdown();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Kiểm tra lại trạng thái tài khoản trước khi thực hiện đổi mật khẩu
                CheckAccountStatus();
                
                if (!ValidateInputs()) return;

                // Kiểm tra mật khẩu hiện tại
                if (!_userService.ValidatePassword(_currentUser.Username, txtCurrentPassword.Password))
                {
                    MessageBox.Show("Mật khẩu hiện tại không chính xác!", 
                                   "Lỗi", 
                                   MessageBoxButton.OK, 
                                   MessageBoxImage.Error);
                    txtCurrentPassword.Focus();
                    return;
                }

                // Cập nhật mật khẩu mới
                _currentUser.Password = txtNewPassword.Password;
                _currentUser.IsFirstLogin = false;
                _userService.UpdateUser(_currentUser);

                // Xóa file lưu thông tin đăng nhập cũ nếu có
                try
                {
                    string log_file = "loginInfo.txt";
                    if (File.Exists(log_file))
                    {
                        File.Delete(log_file);
                    }
                }
                catch { /* Ignore delete errors */ }

                MessageBox.Show("Đổi mật khẩu thành công!", 
                               "Thông báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Information);

                // Chuyển đến dashboard tương ứng
                if (_isForced)
                {
                    ParentDashboard parentDashboard = new ParentDashboard(_currentUser);
                    parentDashboard.Show();
                }
                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", 
                               "Lỗi", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtCurrentPassword.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hiện tại!", 
                               "Thông báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
                txtCurrentPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNewPassword.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu mới!", 
                               "Thông báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
                txtNewPassword.Focus();
                return false;
            }

            if (txtNewPassword.Password.Length < 6)
            {
                MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", 
                               "Thông báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
                txtNewPassword.Focus();
                return false;
            }

            if (txtNewPassword.Password != txtConfirmPassword.Password)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp với mật khẩu mới!", 
                               "Thông báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
                txtConfirmPassword.Focus();
                return false;
            }

            // Kiểm tra mật khẩu mới không giống mật khẩu cũ
            if (txtCurrentPassword.Password == txtNewPassword.Password)
            {
                MessageBox.Show("Mật khẩu mới phải khác mật khẩu cũ!", 
                               "Thông báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
                txtNewPassword.Focus();
                return false;
            }

            return true;
        }
    }
} 