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
using WPF.SchoolMedicalManagementSystem.ManagerView;
using WPF.SchoolMedicalManagementSystem.ParentView;

using System.IO;

using WPF.SchoolMedicalManagementSystem.NurseView;

namespace WPF.SchoolMedicalManagementSystem
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private readonly IUserService _userService;

        // Constants for Role IDs
        private const int ADMIN_ROLE_ID = 1;
        private const int NURSE_ROLE_ID = 2;
        private const int PARENT_ROLE_ID = 3;

        // Constants for Message Box titles
        private const string LOGIN_SUCCESS_TITLE = "Login Successful";
        private const string LOGIN_FAILED_TITLE = "Login Failed";

        public LoginWindow()
        {
            InitializeComponent();
            _userService = new UserService();

            RestoreLoginInformation();

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = txtUsername.Text.Trim();
                string password = txtPassword.Password.Trim();
                
                // Trước tiên, kiểm tra xem có tài khoản nào với username này không
                var userCheck = _userService.GetUserByUsername(username);
                
                if (userCheck != null)
                {
                    // Có tài khoản với username này, kiểm tra xem nó có bị vô hiệu hóa không
                    if (userCheck.IsActive == false)
                    {
                        MessageBox.Show(
                            "Tài khoản của bạn đã bị vô hiệu hóa. Vui lòng liên hệ quản trị viên để kích hoạt lại.",
                            "Tài khoản bị vô hiệu hóa",
                            MessageBoxButton.OK,
                            MessageBoxImage.Warning);
                        return;
                    }
                    
                    // Kiểm tra mật khẩu
                    var user = _userService.GetUser(username, password);
                    
                    if (user != null)
                    {
                        string welcomeMessage = $"Welcome {user.FullName} with {user.Role.RoleName}";

                        // Lưu thông tin đăng nhập nếu người dùng chọn "Remember me"
                        SaveLoginInfo(user, chkRememberMe.IsChecked.Value);

                        switch (user.Role.RoleId)
                        {
                            case ADMIN_ROLE_ID:
                                ManagerDashboard managerDashboard = new ManagerDashboard(user);
                                managerDashboard.Show();
                                this.Close();
                                MessageBox.Show(welcomeMessage, LOGIN_SUCCESS_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                break;

                            case NURSE_ROLE_ID:
                                NurseDashboard nurseDashboard = new NurseDashboard(user);
                                nurseDashboard.Show();
                                this.Close();
                                MessageBox.Show(welcomeMessage, LOGIN_SUCCESS_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                break;

                            case PARENT_ROLE_ID:
                                // Kiểm tra cờ IsFirstLogin
                                if (user.IsFirstLogin == true)
                                {
                                    // Hiển thị cửa sổ đổi mật khẩu
                                    ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(user);
                                    changePasswordWindow.Show();
                                    this.Close();
                                }
                                else
                                {
                                    // Chuyển đến dashboard bình thường
                                    ParentDashboard parentDashboard = new ParentDashboard(user);
                                    parentDashboard.Show();
                                    this.Close();
                                    MessageBox.Show(welcomeMessage, LOGIN_SUCCESS_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                                break;

                            default:
                                MessageBox.Show("Unknown role type.", LOGIN_FAILED_TITLE, MessageBoxButton.OK, MessageBoxImage.Warning);
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không chính xác.", LOGIN_FAILED_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Tài khoản không tồn tại.", LOGIN_FAILED_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred at login: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OnEnterPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }
        }

        private void SaveLoginInfo(User user, bool isRemembered)
        {
            if (!isRemembered)
            {
                // Nếu không chọn Remember me, xóa file thông tin đăng nhập nếu có
                string log_file = "loginInfo.txt";
                if (File.Exists(log_file))
                {
                    try
                    {
                        File.Delete(log_file);
                    }
                    catch { /* Ignore delete errors */ }
                }
                return;
            }
            
            // Lưu thông tin người dùng hiện tại (đã được xác thực)
            string info = user.Username + ";" + user.Password + ";" + isRemembered;

            try
            {
                StreamWriter writer = new StreamWriter("loginInfo.txt", false, Encoding.UTF8);
                writer.WriteLine(info);
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể lưu thông tin đăng nhập: {ex.Message}", 
                               "Cảnh báo", 
                               MessageBoxButton.OK, 
                               MessageBoxImage.Warning);
            }
        }

        private void RestoreLoginInformation()
        {
            string log_file = "loginInfo.txt";
            if (File.Exists(log_file))
            {
                try
                {
                    //Nếu tồn tại file này:
                    StreamReader reader = new StreamReader(log_file);
                    string line = reader.ReadLine();
                    reader.Close();
                    
                    if (!string.IsNullOrEmpty(line))
                    {
                        //Tách line thành 3 thông tin: username; password; save
                        string[] arrData = line.Split(';');
                        if (arrData.Length == 3 && arrData[2] == "True")
                        {
                            txtUsername.Text = arrData[0];
                            txtPassword.Password = arrData[1];
                            chkRememberMe.IsChecked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Nếu có lỗi khi đọc file, bỏ qua việc khôi phục thông tin
                    Console.WriteLine($"Error restoring login info: {ex.Message}");
                }
            }
        }

    }
}