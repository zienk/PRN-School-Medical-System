using System.Windows;
using System.Windows.Controls;

// Đảm bảo namespace này khớp với vị trí của file trong cấu trúc dự án của bạn
// Vì bạn đã di chuyển ReportsPage vào thư mục 'Nurse', namespace sẽ là 'Pages.Nurse'
namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    /// <summary>
    /// Interaction logic for ReportsPage.xaml
    /// </summary>
    public partial class ReportsPage : Page // Đảm bảo là partial class và kế thừa từ Page
    {
        public ReportsPage()
        {
            InitializeComponent(); // Dòng này sẽ được tự động tạo bởi WPF khi build
            // Khởi tạo ViewModel hoặc tải dữ liệu báo cáo ban đầu ở đây nếu cần
            // Ví dụ: DataContext = new ReportsViewModel();
        }

        // Phương thức xử lý sự kiện cho nút "Tạo báo cáo"
        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            // Logic để tạo báo cáo sẽ được thêm vào đây
            MessageBox.Show("Chức năng tạo báo cáo sẽ được thực hiện tại đây.");
            // Ví dụ: Lấy loại báo cáo được chọn và tạo báo cáo tương ứng
            // if (CmbReportType.SelectedItem is ComboBoxItem selectedItem)
            // {
            //     string reportType = selectedItem.Content.ToString();
            //     MessageBox.Show($"Tạo báo cáo loại: {reportType}");
            // }
        }

        // Phương thức xử lý sự kiện khi lựa chọn loại báo cáo thay đổi trong ComboBox
        private void CmbReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Logic khi lựa chọn loại báo cáo thay đổi
            // Ví dụ: Hiển thị các tùy chọn lọc khác nhau tùy thuộc vào loại báo cáo
            // ComboBox cmb = sender as ComboBox;
            // if (cmb != null && cmb.SelectedItem is ComboBoxItem selectedItem)
            // {
            //     string reportType = selectedItem.Content.ToString();
            //     MessageBox.Show($"Loại báo cáo đã chọn: {reportType}");
            // }
        }
    }
}