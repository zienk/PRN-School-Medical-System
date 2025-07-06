using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.UserPage 
{
    /// <summary>
    /// Interaction logic for StudentCheckupResultsPage.xaml
    /// </summary>
    public partial class StudentCheckupResultsPage : Page
    {
        public StudentCheckupResultsPage()
        {
            InitializeComponent();
            // Khởi tạo ViewModel và tải dữ liệu kết quả khám ở đây
            // Ví dụ: DataContext = new StudentCheckupResultsViewModel();
        }

        private void ViewCheckupDetails_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != null && btn.Tag != null)
            {
                int resultId = (int)btn.Tag;
                MessageBox.Show($"Xem chi tiết kết quả khám sức khỏe ID: {resultId}");
                // TODO: Mở một cửa sổ hoặc chuyển đến Page hiển thị chi tiết kết quả khám sức khỏe
            }
        }
    }
}