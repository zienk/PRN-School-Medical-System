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
    /// <summary>
    /// Interaction logic for ManagerDashboard.xaml
    /// </summary>
    public partial class ManagerDashboard : Window
    {
        public ManagerDashboard()
        {
            InitializeComponent();
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            UserManagement user = new UserManagement();
            user.Show();
            this.Close();
        }

        private void btnManageStudents_Click(object sender, RoutedEventArgs e)
        {
            StudentManagement student = new StudentManagement();
            student.Show();
            this.Close();
        }

        private void btnManageHealthRecords_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
