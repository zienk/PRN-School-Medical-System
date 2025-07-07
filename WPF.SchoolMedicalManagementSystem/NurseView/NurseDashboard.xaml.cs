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
    /// <summary>
    /// Interaction logic for NurseDashboard.xaml
    /// </summary>
    public partial class NurseDashboard : Window
    {
        public NurseDashboard()
        {
            InitializeComponent();
        }

        private void btnManageHealthCheckups_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement health checkups management logic
        }

        private void btnManageVaccinations_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement vaccinations management logic
        }

        private void btnManageIncidents_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement medical incidents management logic
        }
    }
}
