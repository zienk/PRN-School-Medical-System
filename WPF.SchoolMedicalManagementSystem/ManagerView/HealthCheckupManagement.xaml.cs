using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

// Thêm các namespace cần thiết cho BusinessObjects.Entities và Services
// using BusinessObjects.Entities; 
// using Services.Implementations;
// using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    public partial class HealthCheckupManagement : Window
    {
        private User currentUser; // Giả định bạn có một lớp User để quản lý thông tin người dùng đăng nhập
        private PrnEduHealthContext _context = new PrnEduHealthContext(); // Giả định bạn có một DbContext để kết nối với cơ sở dữ liệu

        private IHealthCheckupService _healthCheckupService = new HealthCheckupService(); // Giả định bạn đã tạo Service để quản lý HealthCheckup
        private IStudentService _studentService = new StudentService(); // Giả định bạn đã tạo Service để quản lý Student
        private IHealthCheckupResultService _healthCheckupResultService = new HealthCheckupResultService(); // Giả định bạn đã tạo Service để quản lý HealthCheckupResult
        //private CheckupProgram _selectedCheckupProgram;
        private Student _selectedStudentForNewResult; // Đổi tên để rõ ràng hơn

        private bool _isEditMode = false;
        private HealthCheckup _editingCheckupProgram;
        private List<HealthCheckupResult> allHealthResults = new List<HealthCheckupResult>();

        // Khởi tạo Service (giả định đã có hoặc bạn sẽ tạo)
        // private IHealthCheckupResultService _healthCheckupResultService = new HealthCheckupResultService();
        // private IStudentService _studentService = new StudentService(); // Để lấy thông tin FullName của Student


        public HealthCheckupManagement(User user)
        {
            currentUser = user; // Lưu thông tin người dùng đăng nhập
            InitializeComponent();
            LoadCheckupPrograms();
            //LoadStudentsForSelection(); // Tải học sinh cho popup chọn
            //LoadHealthResultsForDisplay(); // Tải kết quả khám để hiển thị trong bảng mới
        }

        private void LoadCheckupPrograms()
        {
            dgCheckupPrograms.ItemsSource = null; // Đặt lại ItemsSource để tránh lỗi khi cập nhật
            dgCheckupPrograms.ItemsSource = _healthCheckupService.GetAllHealthCheckups();
        }

        private void LoadStudentsForSelection() // Đổi tên để phân biệt với việc tải kết quả
        {
            //// Sample data - replace with actual database calls (ví dụ: _studentService.GetAllStudents();)
            //_students.Add(new Student { Id = 1, StudentId = "ST001", FullName = "Nguyen Van An", Class = "10A1", Age = 16 });
            //_students.Add(new Student { Id = 2, StudentId = "ST002", FullName = "Tran Thi Binh", Class = "10A2", Age = 15 });
            //_students.Add(new Student { Id = 3, StudentId = "Le Van Cuong", FullName = "Le Van Cuong", Class = "11B1", Age = 17 });
            //_students.Add(new Student { Id = 4, StudentId = "Pham Thi Dung", FullName = "Pham Thi Dung", Class = "11B2", Age = 16 });
            //_students.Add(new Student { Id = 5, StudentId = "Hoang Van Em", FullName = "Hoang Van Em", Class = "12C1", Age = 18 });
            //_students.Add(new Student { Id = 6, StudentId = "Vu Thi Phuong", FullName = "Vu Thi Phuong", Class = "12C2", Age = 17 });

            //_filteredStudentsForSelection = new ObservableCollection<Student>(_students);
            //// Gán ItemsSource cho DataGrid trong popup
            //dgStudents.ItemsSource = _filteredStudentsForSelection;
        }

        private void LoadHealthResultsForDisplay()
        {
            //// Đây là nơi bạn sẽ gọi Service để lấy dữ liệu từ HealthCheckupResult và Student
            //// Ví dụ: var results = _healthCheckupResultService.GetAllHealthCheckupResults();

            //// Dữ liệu mẫu (thay thế bằng dữ liệu thực từ DB)
            //var sampleResults = new List<HealthResult>
            //{
            //    new HealthResult { CheckupProgramId = 1, StudentId = 1, Height = 160, Weight = 50, Vision = "20/20", DentalStatus = "Good", BloodPressure = "120/80", HeartRate = 70, GeneralCondition = "Healthy", Notes = "No issues", CreatedDate = DateTime.Now.AddDays(-30) },
            //    new HealthResult { CheckupProgramId = 2, StudentId = 2, Height = 155, Weight = 45, Vision = "20/25", DentalStatus = "Cavity", BloodPressure = "110/70", HeartRate = 75, GeneralCondition = "Needs dental follow-up", Notes = "Cavity in molar", CreatedDate = DateTime.Now.AddDays(-15) },
            //    new HealthResult { CheckupProgramId = 1, StudentId = 3, Height = 170, Weight = 65, Vision = "20/20", DentalStatus = "Good", BloodPressure = "130/85", HeartRate = 68, GeneralCondition = "Healthy", Notes = "Active student", CreatedDate = DateTime.Now.AddDays(-40) }
            //};

            //// Kết hợp dữ liệu HealthResult với thông tin Student
            //_healthResultsForDisplay.Clear();
            //foreach (var hr in sampleResults)
            //{
            //    // Tìm thông tin học sinh tương ứng
            //    var student = _students.FirstOrDefault(s => s.Id == hr.StudentId);
            //    if (student != null)
            //    {
            //        _healthResultsForDisplay.Add(new StudentHealthCheckupDisplay
            //        {
            //            Id = hr.Id, // Nếu HealthResult có Id riêng
            //            StudentId = student.StudentId, // Mã số HS string
            //            FullName = student.FullName,
            //            Height = hr.Height,
            //            Weight = hr.Weight,
            //            Vision = hr.Vision,
            //            DentalStatus = hr.DentalStatus,
            //            BloodPressure = hr.BloodPressure,
            //            HeartRate = hr.HeartRate,
            //            GeneralCondition = hr.GeneralCondition,
            //            Notes = hr.Notes,
            //            CheckupProgramName = _checkupPrograms.FirstOrDefault(cp => cp.Id == hr.CheckupProgramId)?.CheckupName // Lấy tên chương trình khám
            //        });
            //    }
            //}
            //_filteredHealthResults = new ObservableCollection<StudentHealthCheckupDisplay>(_healthResultsForDisplay);
            //dgHealthResults.ItemsSource = _filteredHealthResults;
        }

        // Navigation Events (giữ nguyên)
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new ManagerDashboard(currentUser);
            dashboard.Show();
            this.Close();
        }

        private void btnManageUsers_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Manage Users window
            MessageBox.Show("Navigate to Manage Users", "Navigation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnManageStudents_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Manage Students window
            MessageBox.Show("Navigate to Manage Students", "Navigation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnManageHealthRecords_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Manage Health Records window
            MessageBox.Show("Navigate to Manage Health Records", "Navigation", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Checkup Program Management Events (giữ nguyên)
        private void btnAddCheckup_Click(object sender, RoutedEventArgs e)
        {
            ShowCheckupProgramPopup(false);
        }

        private void btnEditCheckup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button?.Tag is HealthCheckup checkupProgram)
            {
                ShowCheckupProgramPopup(true, checkupProgram);
            }
        }

        private void btnDeleteCheckup_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button.Tag is HealthCheckup checkupProgram)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the checkup program '{checkupProgram.CheckupName}'?",
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    int status = _healthCheckupService.GetStatus(checkupProgram);
                    if (status == 3) // Giả định 1 là trạng thái đang hoạt động
                    {
                        MessageBox.Show("Cannot delete the Program is Completed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (status == 4) // Giả định 1 là trạng thái đang hoạt động
                    {
                        MessageBox.Show("Cannot delete the Program is Canceled", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (_healthCheckupService.RemoveHealthCheckup(checkupProgram) == true)
                    {
                        MessageBox.Show("Checkup program deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCheckupPrograms();
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete checkup program.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
            }

        }

        private void btnRefreshPrograms_Click(object sender, RoutedEventArgs e)
        {
            LoadCheckupPrograms();
            MessageBox.Show("Checkup programs refreshed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ShowCheckupProgramPopup(bool isEdit, HealthCheckup checkupProgram = null)
        {
            _isEditMode = isEdit;
            _editingCheckupProgram = checkupProgram;

            if (isEdit && checkupProgram != null)
            {
                popupTitle.Text = "Edit Checkup Program";
                txtPopupCheckupName.Text = checkupProgram.CheckupName;
                dpPopupCheckupDate.SelectedDate = checkupProgram.CheckupDate?.ToDateTime(TimeOnly.MinValue);
                txtPopupDescription.Text = checkupProgram.Description;
            }
            else
            {
                popupTitle.Text = "Add Checkup Program";
                txtPopupCheckupName.Clear();
                dpPopupCheckupDate.SelectedDate = DateTime.Now;
                txtPopupDescription.Clear();
            }

            popupOverlay.Visibility = Visibility.Visible;
        }

        private void btnClosePopup_Click(object sender, RoutedEventArgs e)
        {
            popupOverlay.Visibility = Visibility.Collapsed;
        }

        private void btnPopupSave_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtPopupCheckupName.Text))
            {
                MessageBox.Show("Please enter a checkup program name.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!dpPopupCheckupDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Please select a checkup date.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (_isEditMode && _editingCheckupProgram != null)
            {
                // Update existing program
                _editingCheckupProgram.CheckupName = txtPopupCheckupName.Text;
                _editingCheckupProgram.CheckupDate = DateOnly.FromDateTime(dpPopupCheckupDate.SelectedDate.Value);
                _editingCheckupProgram.Description = txtPopupDescription.Text;
                
                
                _healthCheckupService.UpdateHealthCheckup(_editingCheckupProgram);
                

                MessageBox.Show("Checkup program updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // Add new program
                var newProgram = new HealthCheckup
                {
                    CheckupName = txtPopupCheckupName.Text,
                    CheckupDate = DateOnly.FromDateTime(dpPopupCheckupDate.SelectedDate.Value),
                    Description = txtPopupDescription.Text,
                    CreatedBy = currentUser.UserId, // Giả định bạn có UserId trong lớp User
                    StatusId = 1, // Giả định 1 là trạng thái đang hoạt động
                    IsActive = true // Giả định mặc định là hoạt động
                };
                PrnEduHealthContext _context = new PrnEduHealthContext(); // Khởi tạo DbContext

                _healthCheckupService.AddHealthCheckup(newProgram);
                MessageBox.Show("Checkup program added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            //popupOverlay.Visibility = Visibility.Collapsed;
        }

        private void btnPopupCancel_Click(object sender, RoutedEventArgs e)
        {
            popupOverlay.Visibility = Visibility.Collapsed;
        }

        // Create Results Tab Events
        private void cmbCheckupPrograms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //_selectedCheckupProgram = cmbCheckupPrograms.SelectedItem as CheckupProgram;
        }

        private void btnFindStudent_Click(object sender, RoutedEventArgs e)
        {
            //studentPopupOverlay.Visibility = Visibility.Visible;
        }

        

        // Đã đổi _filteredStudents thành _filteredStudentsForSelection cho rõ ràng
        private void txtSearchStudent_TextChanged(object sender, TextChangedEventArgs e)
        {
            //var searchText = txtSearchStudent.Text.ToLower();

            //if (string.IsNullOrWhiteSpace(searchText))
            //{
            //    _filteredStudentsForSelection.Clear();
            //    foreach (var student in _students)
            //    {
            //        _filteredStudentsForSelection.Add(student);
            //    }
            //}
            //else
            //{
            //    _filteredStudentsForSelection.Clear();
            //    var filtered = _students.Where(s =>
            //        s.FullName.ToLower().Contains(searchText) ||
            //        s.StudentId.ToLower().Contains(searchText) ||
            //        s.Class.ToLower().Contains(searchText));

            //    foreach (var student in filtered)
            //    {
            //        _filteredStudentsForSelection.Add(student);
            //    }
            //}
        }

        

        private void btnCreateResultCheckup_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabCreateResults;
            var button = sender as Button;
            if (button?.Tag is HealthCheckup checkupProgram)
            {
                // Lấy tất cả học sinh
                List<Student> students = _studentService.GetAllStudents();
                //List<HealthCheckupResult> healthCheckupResults = new List<HealthCheckupResult>();
                foreach (var student in students)
                {
                    // Tạo một kết quả kiểm tra sức khỏe mới cho mỗi học sinh
                    var result = new HealthCheckupResult
                    {
                        CheckupId = checkupProgram.CheckupId,
                        //Student = student, // Giả định StudentId là khóa chính của Student
                        StudentId = student.StudentId, // Giả định StudentId là khóa chính của Student
                        Height = 0, // Giá trị mặc định, sẽ cập nhật sau
                        Weight = 0, // Giá trị mặc định, sẽ cập nhật sau
                        Vision = "10/10", // Giá trị mặc định, sẽ cập nhật sau
                        DentalStatus = "Good", // Giá trị mặc định, sẽ cập nhật sau
                        BloodPressure = "120/80", // Giá trị mặc định, sẽ cập nhật sau
                        HeartRate = 70, // Giá trị mặc định, sẽ cập nhật sau
                        GeneralCondition = "Healthy", // Giá trị mặc định, sẽ cập nhật sau
                        Notes = "", // Giá trị mặc định, sẽ cập nhật sau
                        CheckupTime = DateTime.Now,
                    };
                    allHealthResults.Add(result);
                }

                allHealthResults = _healthCheckupResultService.CreateHealthCheckupResultByHealthCheckupId(allHealthResults);

                LoadCheckupPrograms();
                checkupProgram.StatusId = 3;
                _healthCheckupService.UpdateStatus(checkupProgram); // Cập nhật trạng thái của chương trình kiểm tra sức khỏe


                dgHealthResults.ItemsSource = allHealthResults;
            }
            else
            {
                MessageBox.Show("Please select a valid checkup program.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnEditResultCheckup_Click(object sender, RoutedEventArgs e)
        {
            
            var button = sender as Button;
            if (button?.Tag is HealthCheckup checkupProgram)
            {
                dgHealthResults.ItemsSource = _healthCheckupResultService.getAllHealthCheckupResultByHealthCheckupId(checkupProgram);
            }
            
            tabControl.SelectedItem = tabCreateResults;

        }

        

        private void btnSaveResult_Click(object sender, RoutedEventArgs e)
        {
            var result = dgHealthResults.ItemsSource as List<HealthCheckupResult>;
            if (result != null)
            {
                foreach (var item in result)
                {
                    // Cập nhật từng kết quả kiểm tra sức khỏe
                    _healthCheckupResultService.UpdateHealthCheckupResult(item);
                }
            }
        }


        // Method to switch to Create Results tab programmatically (giữ nguyên)
        public void SwitchToCreateResultsTab()
        {
            //tabControl.SelectedItem = tabCreateResults;
        }

        private void txtSearchHealthResults_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void TxtSearchHealthResults_TextChanged(object sender, TextChangedEventArgs e)
        {
            //string searchText = txtSearchHealthResults.Text.Trim().ToLower();

            //if (string.IsNullOrEmpty(searchText) || searchText == "tìm kiếm học sinh theo tên, mã học sinh")
            //{
            //    dgHealthResults.ItemsSource = allHealthResults;
            //    return;
            //}

            //var filtered = allHealthResults.Where(r =>
            //    r.StudentId.ToString().ToLower().Contains(searchText) ||
            //    (r.Student != null && !string.IsNullOrEmpty(r.Student.FullName) && r.Student.FullName.ToLower().Contains(searchText))
            //).ToList();

            //dgHealthResults.ItemsSource = filtered;
        }

    }









}