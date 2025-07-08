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
using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    /// <summary>
    /// Interaction logic for StudentManagement.xaml
    /// </summary>
    public partial class StudentManagement : Window
    {

        private readonly IStudentService _studentService;
        public StudentManagement()
        {
            InitializeComponent();

            _studentService = new StudentService();

            LoadData();
        }

        public void LoadData()
        {
            dgStudents.ItemsSource = null;
            dgStudents.ItemsSource = _studentService.GetAllStudents();
        }

        private void btnBackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
            this.Close();
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement manage users logic
        }

        private void btnManageStudents_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement manage students logic
        }

        private void btnManageHealthRecords_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement manage health records logic
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // TODO: Implement search logic on key up
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement search logic on button click
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement add new student logic
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement edit student logic
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement delete student logic
        }
    }
}
