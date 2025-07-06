using System.Windows;
using WPF.SchoolMedicalManagementSystem.ViewModels;
using BusinessObjects.Entities;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class CreateCampaignWindow : Window
    {
        public CreateCampaignWindow(string currentUserId, Action<HealthCheckup> onSaved)
        {
            InitializeComponent();
            var vm = new CreateHealthCheckupViewModel(currentUserId);
            vm.OnSaved = onSaved;
            vm.CloseAction = this.Close;
            DataContext = vm;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Xử lý lưu dữ liệu ở đây
            // Ví dụ: Validate và trả về dữ liệu cho trang chính
            this.DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}