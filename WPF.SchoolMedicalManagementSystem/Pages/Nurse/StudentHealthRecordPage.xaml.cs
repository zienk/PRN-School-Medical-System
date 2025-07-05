using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class StudentHealthRecordPage : Page
    {
        public StudentHealthRecordPage()
        {
            InitializeComponent();
            LoadStudents(); // Hàm này sẽ tải dữ liệu học sinh
        }

        private void LoadStudents()
        {
            // Logic để tải danh sách học sinh từ database và gán vào dgStudents.ItemsSource
        }

        private void AddRecord_Click(object sender, RoutedEventArgs e)
        {
            // Logic để mở cửa sổ/dialog thêm hồ sơ mới
        }

        private void EditRecord_Click(object sender, RoutedEventArgs e)
        {
            // Logic để sửa hồ sơ sức khỏe của học sinh được chọn
        }

        private void ViewDetails_Click(object sender, RoutedEventArgs e)
        {
            // Logic để xem chi tiết hồ sơ sức khỏe, sự cố y tế, kết quả khám/tiêm của học sinh
        }

        private void DeleteRecord_Click(object sender, RoutedEventArgs e)
        {
            // Logic để xóa hồ sơ học sinh
        }

        private void SearchRecord_Click(object sender, RoutedEventArgs e)
        {
            // Logic tìm kiếm học sinh theo tên
        }

        private void DgStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Xử lý khi chọn một hàng trong DataGrid (nếu cần)
        }
    }
}