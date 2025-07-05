using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.User // ĐÃ THAY ĐỔI
{
    /// <summary>
    /// Interaction logic for StudentIncidentListPage.xaml
    /// </summary>
    public partial class StudentIncidentListPage : Page
    {
        public StudentIncidentListPage()
        {
            InitializeComponent();
            // Khởi tạo ViewModel và tải dữ liệu sự cố ở đây
            // Ví dụ: DataContext = new StudentIncidentListViewModel();
        }

        private void ViewIncidentDetails_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                int incidentId = (int)btn.Tag;
                MessageBox.Show($"Xem chi tiết sự cố ID: {incidentId}");
                // TODO: Mở một cửa sổ hoặc chuyển đến Page hiển thị chi tiết sự cố
            }
        }
    }
}