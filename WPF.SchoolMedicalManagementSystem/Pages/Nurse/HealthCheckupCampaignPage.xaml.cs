using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class HealthCheckupCampaignPage : Page
    {
        public HealthCheckupCampaignPage()
        {
            InitializeComponent();
            LoadCampaigns(); // Tải dữ liệu chiến dịch
        }

        private void LoadCampaigns()
        {
            // Logic để tải danh sách chiến dịch khám sức khỏe và gán vào dgHealthCheckupCampaigns.ItemsSource
        }

        private void CreateCampaign_Click(object sender, RoutedEventArgs e)
        {
            // Logic để mở cửa sổ/dialog tạo chiến dịch mới
        }

        private void RecordResult_Click(object sender, RoutedEventArgs e)
        {
            // Logic để ghi nhận kết quả khám sức khỏe cho một chiến dịch
        }

        private void EditCampaign_Click(object sender, RoutedEventArgs e)
        {
            // Logic để sửa thông tin chiến dịch
        }

        private void DeleteCampaign_Click(object sender, RoutedEventArgs e)
        {
            // Logic để xóa chiến dịch
        }

        private void SearchCampaign_Click(object sender, RoutedEventArgs e)
        {
            // Logic tìm kiếm chiến dịch
        }

        private void DgHealthCheckupCampaigns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Xử lý khi chọn một hàng
        }
    }
}