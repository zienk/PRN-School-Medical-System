using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.UserPage
{
    /// <summary>
    /// Interaction logic for StudentVaccinationResultsPage.xaml
    /// </summary>
    public partial class StudentVaccinationResultsPage : Page
    {
        public StudentVaccinationResultsPage()
        {
            InitializeComponent();
            // Khởi tạo ViewModel và tải dữ liệu tiêm chủng ở đây
            // Ví dụ: DataContext = new StudentVaccinationResultsViewModel();
        }

        private void ViewVaccinationDetails_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                int recordId = (int)btn.Tag;
                MessageBox.Show($"Xem chi tiết kết quả tiêm chủng ID: {recordId}");
                // TODO: Mở một cửa sổ hoặc chuyển đến Page hiển thị chi tiết kết quả tiêm chủng
            }
        }
    }
}