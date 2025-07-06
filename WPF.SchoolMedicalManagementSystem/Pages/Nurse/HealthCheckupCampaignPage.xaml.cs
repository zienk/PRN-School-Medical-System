using System;
using System.Windows;
using System.Windows.Controls;
using WPF.SchoolMedicalManagementSystem.ViewModels;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class HealthCheckupCampaignPage : Page
    {

        public HealthCheckupCampaignPage(string currentUserId)
        {
            InitializeComponent();
            DataContext = new HealthCheckupCampaignPageViewModel(currentUserId);
        }
    }
}