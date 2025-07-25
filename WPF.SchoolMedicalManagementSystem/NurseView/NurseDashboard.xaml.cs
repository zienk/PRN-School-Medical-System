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
using BusinessObjects.Entities;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for NurseDashboard.xaml
    /// </summary>
    public partial class NurseDashboard : Window
    {
        private User currentUser;
        public NurseDashboard(User user)
        {
            InitializeComponent();
            currentUser = user;
        }

        private void btnManageHealthCheckups_Click(object sender, RoutedEventArgs e)
        {
            HealthCheckupManagement healthCheckupManagement = new HealthCheckupManagement(currentUser);
            healthCheckupManagement.Show();
            this.Close();
        }

        private void btnManageVaccinations_Click(object sender, RoutedEventArgs e)
        {
            var vaccinationWindow = new Vaccination(currentUser.UserId);
            vaccinationWindow.Show();
            this.Close();
        }

        private void btnManageIncidents_Click(object sender, RoutedEventArgs e)
        {
            var medicalEventWindow = new MedicalEvent();
            medicalEventWindow.Show();
            this.Close();
        }
    }
}
