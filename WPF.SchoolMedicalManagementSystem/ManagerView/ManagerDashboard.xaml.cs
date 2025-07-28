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

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    // Bảng điều khiển quản lý chính
    public partial class ManagerDashboard : Window
    {
        private User currentUser;
        
        public ManagerDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
        }
        
        public ManagerDashboard()
        {
            InitializeComponent(); 
        }
        
        // Xử lý sự kiện click nút quản lý người dùng
        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            UserManagement userManagement = new UserManagement();
            userManagement.Show();
            this.Close();
        }

        // Xử lý sự kiện click nút quản lý học sinh
        private void btnManageStudents_Click(object sender, RoutedEventArgs e)
        {
            StudentManagement student = new StudentManagement();
            student.Show();
            this.Close();
        }
    }
}
