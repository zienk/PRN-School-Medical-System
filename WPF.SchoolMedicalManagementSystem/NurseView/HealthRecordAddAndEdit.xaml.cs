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
using Services.Interfaces;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for HealthRecordAddAndEdit.xaml
    /// </summary>
    public partial class HealthRecordAddAndEdit : Window
    {
        private readonly IHealthRecordService _healthRecordService;
        private readonly IStudentService _studentService;
        private readonly bool _isEditMode;
        private HealthRecord _healthRecord;
        private List<Student> _students;

        public HealthRecordAddAndEdit(HealthRecord healthRecord = null)
        {
            InitializeComponent();
            _healthRecordService = new HealthRecordService();
            _studentService = new StudentService();
            _isEditMode = healthRecord != null;
            _healthRecord = healthRecord ?? new HealthRecord();

            Loaded += HealthRecordAddAndEdit_Loaded;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => Close();
            cbStudent.SelectionChanged += CbStudent_SelectionChanged;
        }

        private void HealthRecordAddAndEdit_Loaded(object sender, RoutedEventArgs e)
        {
            if (_isEditMode)
            {
                cbStudent.IsEnabled = false;
                if (_healthRecord.Student != null)
                {
                    DisplayStudentInfo(_healthRecord.Student);
                }
                txtHeight.Text = _healthRecord.Height?.ToString() ?? string.Empty;
                txtWeight.Text = _healthRecord.Weight?.ToString() ?? string.Empty;
                txtChronicDiseases.Text = _healthRecord.ChronicDiseases;
                txtAllergies.Text = _healthRecord.Allergies;
                txtNotes.Text = _healthRecord.Notes;
                cbStudent.ItemsSource = new List<Student> { _healthRecord.Student };
                cbStudent.SelectedItem = _healthRecord.Student;
                HeaderTitle.Text = "CẬP NHẬT HỒ SƠ SỨC KHỎE";
            }
            else
            {
                cbStudent.IsEnabled = true;
                _students = _studentService.GetAllStudents();
                var studentsWithoutHealthRecord = _students.Where(s => s.HealthRecord == null).ToList();
                if (studentsWithoutHealthRecord.Count == 0)
                {
                    MessageBox.Show("Tất cả học sinh đã có hồ sơ sức khỏe!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    return;
                }
                cbStudent.ItemsSource = studentsWithoutHealthRecord;
                cbStudent.SelectedIndex = -1;
                ClearStudentInfo();
                txtHeight.Text = string.Empty;
                txtWeight.Text = string.Empty;
                txtChronicDiseases.Text = string.Empty;
                txtAllergies.Text = string.Empty;
                txtNotes.Text = string.Empty;
                HeaderTitle.Text = "THÊM HỒ SƠ SỨC KHỎE MỚI";
            }
        }

        private void CbStudent_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cbStudent.SelectedItem is Student student)
            {
                // Kiểm tra nếu học sinh đã có HealthRecord thì cảnh báo và reset selection
                if (_isEditMode == false && student.HealthRecord != null)
                {
                    MessageBox.Show("Học sinh này đã có hồ sơ sức khỏe!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    cbStudent.SelectedIndex = -1;
                    ClearStudentInfo();
                    return;
                }
                DisplayStudentInfo(student);
            }
            else
            {
                ClearStudentInfo();
            }
        }

        private void DisplayStudentInfo(Student student)
        {
            txtStudentFullName.Text = student.FullName;
            txtStudentDOB.Text = student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "";
            txtStudentGender.Text = student.Gender?.GenderName ?? "";
            txtStudentClass.Text = student.Class;
            txtParentName.Text = student.Parent?.FullName ?? "";
        }

        private void ClearStudentInfo()
        {
            txtStudentFullName.Text = "";
            txtStudentDOB.Text = "";
            txtStudentGender.Text = "";
            txtStudentClass.Text = "";
            txtParentName.Text = "";
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            // Validate
            if (!_isEditMode && cbStudent.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn học sinh.");
                return;
            }
            if (!_isEditMode && cbStudent.SelectedItem is Student selectedStudent && selectedStudent.HealthRecord != null)
            {
                MessageBox.Show("Học sinh này đã có hồ sơ sức khỏe!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            decimal? height = null, weight = null;
            if (!string.IsNullOrWhiteSpace(txtHeight.Text))
            {
                if (!decimal.TryParse(txtHeight.Text, out var h))
                {
                    MessageBox.Show("Chiều cao không hợp lệ.");
                    return;
                }
                height = h;
            }
            if (!string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                if (!decimal.TryParse(txtWeight.Text, out var w))
                {
                    MessageBox.Show("Cân nặng không hợp lệ.");
                    return;
                }
                weight = w;
            }
            // Gán dữ liệu
            if (!_isEditMode)
            {
                var student = (Student)cbStudent.SelectedItem;
                _healthRecord.StudentId = student.StudentId;
            }
            _healthRecord.Height = height;
            _healthRecord.Weight = weight;
            _healthRecord.ChronicDiseases = txtChronicDiseases.Text.Trim();
            _healthRecord.Allergies = txtAllergies.Text.Trim();
            _healthRecord.Notes = txtNotes.Text.Trim();

            try
            {
                if (_isEditMode)
                {
                    _healthRecordService.UpdateHealthRecord(_healthRecord);
                    MessageBox.Show("Cập nhật hồ sơ sức khỏe thành công!");
                }
                else
                {
                    _healthRecordService.CreateHealthRecord(_healthRecord);
                    MessageBox.Show("Thêm hồ sơ sức khỏe thành công!");
                }
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
}
