using System;
using System.Windows;
using System.Windows.Controls;
using WPF.SchoolMedicalManagementSystem.ViewModels;
using WPF.SchoolMedicalManagementSystem.Views;
// Các namespace cho các trang của bạn
using WPF.SchoolMedicalManagementSystem.Pages; // Giữ lại nếu bạn có HomePage.xaml gốc hoặc các Page khác không thuộc thư mục con nào
using WPF.SchoolMedicalManagementSystem.Pages.UserPage; // Cho các trang StudentHealthProfilePage, StudentIncidentListPage, v.v.
using WPF.SchoolMedicalManagementSystem.Pages.Nurse; // Namespace MỚI cho các trang đã di chuyển

namespace WPF.SchoolMedicalManagementSystem
{
    public partial class MainWindow : Window
    {
        public int UserRoleID { get; private set; }

        public MainWindow(LoginWindowViewModels currentUserViewModel)
        {
            InitializeComponent();

            if (currentUserViewModel != null)
            {
                this.UserRoleID = currentUserViewModel.Roleid;
            }
            else
            {
                this.UserRoleID = 0;
            }

            ApplyUserRoleVisibility();

            // Điều hướng đến trang mặc định hoặc trang chào mừng dựa trên vai trò
            if (UserRoleID == 3) // Nếu là vai trò User/Parent
            {
                MainFrame.Navigate(new Uri("Pages/User/StudentHealthProfilePage.xaml", UriKind.Relative));
            }
            else // Mặc định cho Admin/Nurse hoặc nếu không có vai trò cụ thể
            {
                // HomePage đã được di chuyển vào Pages/Nurse
                MainFrame.Navigate(new Uri("Pages/Nurse/HomePage.xaml", UriKind.Relative));
            }
        }

        private void ApplyUserRoleVisibility()
        {
            // Đảm bảo tất cả các nút đều có x:Name và Visibility="Collapsed" trong MainWindow.xaml
            btnManageStudentRecords.Visibility = Visibility.Collapsed;
            btnManageAccounts.Visibility = Visibility.Collapsed;
            btnCreateVaccinationCampaign.Visibility = Visibility.Collapsed;
            btnCreateHealthCheckupCampaign.Visibility = Visibility.Collapsed;
            btnReportsAndStatistics.Visibility = Visibility.Collapsed;
            btnHealthProfile.Visibility = Visibility.Collapsed;
            btnIncidents.Visibility = Visibility.Collapsed;
            btnVaccinationResults.Visibility = Visibility.Collapsed;
            btnCheckupResults.Visibility = Visibility.Collapsed;


            switch (UserRoleID)
            {
                case 1: // Admin
                    btnManageStudentRecords.Visibility = Visibility.Visible;
                    btnManageAccounts.Visibility = Visibility.Visible;
                    btnCreateVaccinationCampaign.Visibility = Visibility.Visible;
                    btnCreateHealthCheckupCampaign.Visibility = Visibility.Visible;
                    btnReportsAndStatistics.Visibility = Visibility.Visible;
                    break;
                case 2: // Nurse (Y tá)
                    btnManageStudentRecords.Visibility = Visibility.Visible;
                    btnCreateVaccinationCampaign.Visibility = Visibility.Visible;
                    btnCreateHealthCheckupCampaign.Visibility = Visibility.Visible;
                    btnReportsAndStatistics.Visibility = Visibility.Visible;
                    break;
                case 3: // Parent/User
                    btnHealthProfile.Visibility = Visibility.Visible;
                    btnIncidents.Visibility = Visibility.Visible;
                    btnVaccinationResults.Visibility = Visibility.Visible;
                    btnCheckupResults.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        // --- Cập nhật các phương thức điều hướng ---

        private void ManageStudentRecords_Click(object sender, RoutedEventArgs e)
        {
            // Đường dẫn mới
            MainFrame.Navigate(new Uri("Pages/Nurse/StudentHealthRecordPage.xaml", UriKind.Relative));
        }

        private void ManageAccounts_Click(object sender, RoutedEventArgs e)
        {
            // Đường dẫn mới
            MainFrame.Navigate(new Uri("Pages/Nurse/UserManagementPage.xaml", UriKind.Relative));
        }

        private void CreateVaccinationCampaign_Click(object sender, RoutedEventArgs e)
        {
            // Đường dẫn mới
            MainFrame.Navigate(new Uri("Pages/Nurse/VaccinationCampaignPage.xaml", UriKind.Relative));
        }

        private void CreateHealthCheckupCampaign_Click(object sender, RoutedEventArgs e)
        {
            // Đường dẫn mới
            MainFrame.Navigate(new Uri("Pages/Nurse/HealthCheckupCampaignPage.xaml", UriKind.Relative));
        }

        private void ReportsAndStatistics_Click(object sender, RoutedEventArgs e)
        {
            // Đường dẫn mới
            MainFrame.Navigate(new Uri("Pages/Nurse/ReportsPage.xaml", UriKind.Relative));
        }

        // --- Các phương thức điều hướng cho User không thay đổi đường dẫn ---
        private void NavigateToHealthProfile(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/User/StudentHealthProfilePage.xaml", UriKind.Relative));
        }

        private void NavigateToIncidents(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/User/StudentIncidentListPage.xaml", UriKind.Relative));
        }

        private void NavigateToVaccinationResults(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/User/StudentVaccinationResultsPage.xaml", UriKind.Relative));
        }

        private void NavigateToCheckupResults(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Uri("Pages/UserPage/StudentCheckupResultsPage.xaml", UriKind.Relative));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}