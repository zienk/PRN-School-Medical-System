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

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    /// <summary>
    /// Interaction logic for StudentInfo.xaml
    /// </summary>
    public partial class StudentInfo : Window
    {
        private List<Student> children;
        private User parentUser;
        private Student currentStudent;
        private int selectedIndex = 0;
        private StudentService studentService = new StudentService();

        public StudentInfo(List<Student> students, User user)
        {
            InitializeComponent();
            children = students;
            parentUser = user;
            selectedIndex = 0;
            currentStudent = children[selectedIndex];
            LoadStudentTabs();
            LoadStudentData();
        }

        public StudentInfo() : this(new List<Student> { new Student() }, new User()) { }

        private void LoadStudentTabs()
        {
            StudentTabs.Items.Clear();
            for (int i = 0; i < children.Count; i++)
            {
                var btn = new Button
                {
                    Content = $"{(children[i].GenderId == 1 ? "👦" : "👧")} {children[i].FullName}",
                    Margin = new Thickness(5, 0, 0, 0),
                    Padding = new Thickness(20, 12, 20, 12),
                    Tag = i,
                    Background = (i == selectedIndex) ? new SolidColorBrush(Color.FromRgb(66, 133, 244)) : Brushes.Transparent,
                    Foreground = (i == selectedIndex) ? Brushes.White : new SolidColorBrush(Color.FromRgb(95, 99, 104)),
                    BorderThickness = new Thickness(0),
                    Cursor = Cursors.Hand,
                    MinWidth = 120
                };
                btn.Click += StudentTab_Click;
                StudentTabs.Items.Add(btn);
            }
        }

        private void StudentTab_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int idx)
            {
                selectedIndex = idx;
                currentStudent = children[selectedIndex];
                LoadStudentTabs(); // Cập nhật style tab đang chọn
                LoadStudentData();
            }
        }

        private void LoadStudentData()
        {
            if (currentStudent != null)
            {
                txtFullName.Text = currentStudent.FullName ?? "";
                dpDateOfBirth.SelectedDate = currentStudent.DateOfBirth?.ToDateTime(TimeOnly.MinValue);
                if (currentStudent.GenderId == 1)
                    cbGender.SelectedIndex = 1; // Male
                else if (currentStudent.GenderId == 2)
                    cbGender.SelectedIndex = 0; // Female
                else
                    cbGender.SelectedIndex = 2; // Other
                // Fill HealthRecord
                if (currentStudent.HealthRecord != null)
                {
                    txtHeight.Text = currentStudent.HealthRecord.Height?.ToString() ?? "";
                    txtWeight.Text = currentStudent.HealthRecord.Weight?.ToString() ?? "";
                    txtChronicDiseases.Text = currentStudent.HealthRecord.ChronicDiseases ?? "";
                    txtAllergies.Text = currentStudent.HealthRecord.Allergies ?? "";
                    txtNotes.Text = currentStudent.HealthRecord.Notes ?? "";
                }
                else
                {
                    txtHeight.Text = "";
                    txtWeight.Text = "";
                    txtChronicDiseases.Text = "";
                    txtAllergies.Text = "";
                    txtNotes.Text = "";
                }
                // Cập nhật header động
                txtStudentName.Text = currentStudent.FullName ?? "";
                string className = currentStudent.Class ?? "";
                string ageText = "";
                if (currentStudent.DateOfBirth.HasValue)
                {
                    var today = DateTime.Today;
                    var dob = currentStudent.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue);
                    int age = today.Year - dob.Year;
                    if (dob > today.AddYears(-age)) age--;
                    ageText = $"Age {age} years old";
                }
                else
                {
                    ageText = "Age N/A";
                }
                txtStudentGradeAge.Text = $"{className} • {ageText}";
                txtStudentClass.Text = $"Class {className} • Student ID: ST{currentStudent.StudentId:D6}";
            }
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các control nhập liệu
            currentStudent.FullName = txtFullName.Text;
            // GenderId: 1=Male, 2=Female, 3=Other (giả định)
            if (cbGender.SelectedIndex == 1)
                currentStudent.GenderId = 1;
            else if (cbGender.SelectedIndex == 0)
                currentStudent.GenderId = 2;
            else
                currentStudent.GenderId = 3;
            currentStudent.DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null;
            currentStudent.ParentId = parentUser.UserId;
            currentStudent.IsActive = true;

            // Cập nhật HealthRecord
            if (currentStudent.HealthRecord == null)
                currentStudent.HealthRecord = new HealthRecord();

            if (decimal.TryParse(txtHeight.Text, out decimal height))
                currentStudent.HealthRecord.Height = height;
            else
                currentStudent.HealthRecord.Height = null;

            if (decimal.TryParse(txtWeight.Text, out decimal weight))
                currentStudent.HealthRecord.Weight = weight;
            else
                currentStudent.HealthRecord.Weight = null;

            currentStudent.HealthRecord.ChronicDiseases = txtChronicDiseases.Text;
            currentStudent.HealthRecord.Allergies = txtAllergies.Text;
            currentStudent.HealthRecord.Notes = txtNotes.Text;
            currentStudent.HealthRecord.LastUpdatedDate = DateTime.Now;
            currentStudent.HealthRecord.IsActive = true;
            currentStudent.HealthRecord.StudentId = currentStudent.StudentId;

            try
            {
                studentService.UpdateStudent(currentStudent);
                MessageBox.Show("Cập nhật thông tin thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Cập nhật thất bại: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
