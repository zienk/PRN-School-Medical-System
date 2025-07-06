using System.Collections.ObjectModel;
using System.Windows.Input;
using DataAccessLayer;
using BusinessObjects.Entities; 

namespace WPF.SchoolMedicalManagementSystem.ViewModels
{
    public class HealthCheckupCampaignPageViewModel : BaseViewModel
    {
        private readonly string _currentUserId;
        public ObservableCollection<HealthCheckup> Campaigns { get; set; } = new();

        public ICommand AddCampaignCommand { get; }

        public HealthCheckupCampaignPageViewModel(string currentUserId)
        {
           _currentUserId = currentUserId;
            AddCampaignCommand = new RelayCommand(_ => AddCampaign());
            // Load dữ liệu ban đầu nếu cần
            PrnEduHealthContext context = new PrnEduHealthContext();
            var campaigns = context.HealthCheckups.ToList();
            foreach (var campaign in campaigns)
            {
                Campaigns.Add(campaign);
            }
        }

        private void AddCampaign()
        {
            var window = new Pages.Nurse.CreateCampaignWindow(_currentUserId, campaign =>
            {
                if (campaign != null)
                {
                    Campaigns.Add(campaign);
                    // TODO: Lưu vào DB nếu cần
                }
            });
            window.ShowDialog();
        }
    }
}