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
                var user = _userService.GetUser(username, password);

                if (user != null)
                {
                    string welcomeMessage = $"Welcome {user.FullName} with {user.Role.RoleName}";

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
                            NurseDashboard studentRecordManagement = new NurseDashboard(user);
                            studentRecordManagement.Show();
                            this.Close();

                            MessageBox.Show(welcomeMessage, LOGIN_SUCCESS_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                        case PARENT_ROLE_ID:
                            ParentDashboard parentDashboard = new ParentDashboard(user);
                            parentDashboard.Show();
                            this.Close();
                            MessageBox.Show(welcomeMessage, LOGIN_SUCCESS_TITLE, MessageBoxButton.OK, MessageBoxImage.Information);
                            break;

                        default:
                            MessageBox.Show("Unknown role type.", LOGIN_FAILED_TITLE, MessageBoxButton.OK, MessageBoxImage.Warning);
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid username or password.", LOGIN_FAILED_TITLE, MessageBoxButton.OK, MessageBoxImage.Error);
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
            string info = user.Username + ";" + user.Password + ";" + isRemembered;

            StreamWriter writer = new StreamWriter("loginInfo.txt", false, Encoding.UTF8);

            writer.WriteLine(info);
            writer.Close();
        }

        private void RestoreLoginInformation()
        {
            string log_file = "loginInfo.txt";
            if (File.Exists(log_file))
            {
                //Nếu tồn tại file này:
                StreamReader reader = new StreamReader(log_file);
                string line = reader.ReadLine();
                reader.Close();
                //Tách line thành 3 thông tin: email; password; save
                string[] arrData = line.Split(';');
                if (arrData.Length == 3 && arrData[2] == "True")
                {
                    txtUsername.Text = arrData[0];
                    txtPassword.Password = arrData[1];
                    chkRememberMe.IsChecked = true;
                }
            }
        }

    }
}