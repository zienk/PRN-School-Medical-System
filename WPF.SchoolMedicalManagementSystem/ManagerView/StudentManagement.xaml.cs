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
using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    // Giao diện quản lý học sinh
    public partial class StudentManagement : Window
    {
        private readonly IStudentService _studentService;

        public StudentManagement()
        {
            InitializeComponent();
            _studentService = new StudentService();
            LoadData();
            UpdateRecordCount();
        }

        // Tải dữ liệu học sinh vào DataGrid
        private void LoadData()
        {
            dgStudents.ItemsSource = null;
            dgStudents.ItemsSource = _studentService.GetAllStudents();
        }

        // Xử lý sự kiện tìm kiếm học sinh
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchTextBox.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                dgStudents.ItemsSource = _studentService.SearchStudents(searchText);
            }
            else
            {
                LoadData();
            }
            UpdateRecordCount();
        }

        // Xử lý sự kiện thêm học sinh mới
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var studentDialog = new StudentAddAndEdit(isEdit: false, student: null);
                studentDialog.Owner = this;

                var result = studentDialog.ShowDialog();

                if (result == true)
                {
                    LoadData();
                    StatusLabel.Text = "Thêm học sinh thành công!";
                    UpdateRecordCount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi mở form thêm học sinh: {ex.Message}",
                               "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện sửa thông tin học sinh
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedStudent = dgStudents.SelectedItem as Student;

                if (selectedStudent == null)
                {
                    MessageBox.Show("Vui lòng chọn học sinh cần sửa!",
                                   "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var studentDialog = new StudentAddAndEdit(isEdit: true, student: selectedStudent);
                studentDialog.Owner = this;

                var result = studentDialog.ShowDialog();

                if (result == true)
                {
                    LoadData();
                    StatusLabel.Text = "Cập nhật học sinh thành công!";
                    UpdateRecordCount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi khi mở form sửa học sinh: {ex.Message}",
                               "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện ẩn học sinh (soft delete)
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedStudent = dgStudents.SelectedItem as Student;
            if (selectedStudent != null)
            {
                var result = MessageBox.Show(
                    $"Bạn có chắc chắn muốn ẩn học sinh '{selectedStudent.FullName}' không?\nLưu ý: Thao tác này sẽ ẩn cả các bản ghi liên quan (hồ sơ y tế, sự cố, kết quả kiểm tra sức khỏe, hồ sơ tiêm chủng).",
                    "Xác nhận ẩn",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        bool success = _studentService.SoftDeleteStudent(selectedStudent.StudentId);
                        if (success)
                        {
                            LoadData();
                            MessageBox.Show("Ẩn học sinh thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                            UpdateRecordCount();
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy học sinh để ẩn!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Đã xảy ra lỗi khi ẩn: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn học sinh cần ẩn!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Xử lý sự kiện nhấn phím trong ô tìm kiếm
        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            // Có thể thêm tìm kiếm tự động khi gõ
        }

        // Cập nhật số lượng bản ghi
        private void UpdateRecordCount()
        {
            var studentCount = dgStudents.Items.Count;
            RecordCountLabel.Text = $"Tổng: {studentCount} học sinh";
        }

        // Quay lại dashboard
        private void btnBackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            ManagerDashboard managerDashboard = new ManagerDashboard();
            managerDashboard.Show();
            this.Close();
        }
    }
}
