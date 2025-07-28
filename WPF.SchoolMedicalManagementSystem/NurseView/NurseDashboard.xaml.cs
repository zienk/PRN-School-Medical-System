using BusinessObjects.Entities;
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

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    // Bảng điều khiển chính cho y tá
    public partial class NurseDashboard : Window
    {
        private User currentUser;
        
        public NurseDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
            
            // Khởi tạo giá trị giao diện
            if (currentUser != null)
            {
                // Có thể thiết lập thông báo chào mừng nếu cần
            }
            
            LoadData();
        }
        
        // Tải dữ liệu dashboard
        private void LoadData()
        {
            // Tải thống kê dashboard nếu cần
        }

        // Xử lý sự kiện quản lý đợt tiêm chủng
        private void btnManageVaccinations_Click(object sender, RoutedEventArgs e)
        {
            var vaccinationCampaignManagement = new VaccinationCampaignManagement(currentUser);
            vaccinationCampaignManagement.Show();
            this.Close();
        }

        // Xử lý sự kiện quản lý sự cố y tế
        private void btnManageIncidents_Click(object sender, RoutedEventArgs e)
        {
            var medicalEventWindow = new MedicalEvent(currentUser);
            medicalEventWindow.Show();
            this.Close();
        }

        // Xử lý sự kiện quản lý hồ sơ sức khỏe
        private void btnManageHealthRecords_Click(object sender, RoutedEventArgs e)
        {
            var healthRecordsWindow = new StudentRecordManagement(currentUser);
            healthRecordsWindow.Show();
            this.Close();
        }
        
        // Xử lý sự kiện quản lý khám sức khỏe
        private void btnManageHealthCheckups_Click(object sender, RoutedEventArgs e)
        {
            var healthCheckupWindow = new HealthCheckupManagement(currentUser);
            healthCheckupWindow.Show();
            this.Close();
        }

        // Xử lý sự kiện quản lý khám sức khỏe (phương thức thay thế)
        private void btnManageHealthCheckups_Click_1(object sender, RoutedEventArgs e)
        {
            var healthCheckupWindow = new HealthCheckupManagement(currentUser);
            healthCheckupWindow.Show();
            this.Close();
        }

        // Xử lý sự kiện quản lý báo cáo tiêm chủng
        private void btnManageVaccinationRecords_Click(object sender, RoutedEventArgs e)
        {
            var vaccinationRecordsWindow = new VaccinationRecordManagement(currentUser);
            vaccinationRecordsWindow.Show();
            this.Close();
        }
    }
}
