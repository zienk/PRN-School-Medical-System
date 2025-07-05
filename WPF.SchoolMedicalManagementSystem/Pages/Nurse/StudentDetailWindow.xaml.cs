using System.Windows;
using System.Windows.Controls;

namespace WPF.SchoolMedicalManagementSystem.Pages.Nurse
{
    public partial class StudentDetailWindow : Window
    {
        private int _studentId;

        public StudentDetailWindow(int studentId)
        {
            InitializeComponent();
            _studentId = studentId;
            LoadStudentDetails();
        }

        private void LoadStudentDetails()
        {
            // Logic để tải thông tin chi tiết của học sinh (_studentId) từ các bảng HealthRecord, HealthCheckupResult, VaccinationRecord, Incident
            // và hiển thị lên các TextBox/DataGrid tương ứng.
            // Ví dụ:
            // txtStudentFullName.Text = "Chi tiết hồ sơ: " + student.FullName;
            // txtHeight.Text = studentHealthRecord.Height.ToString();
            // dgHealthCheckupResults.ItemsSource = listHealthCheckupResults;
            // dgVaccinationRecords.ItemsSource = listVaccinationRecords;
            // dgIncidentRecords.ItemsSource = listIncidentRecords;
        }
    }
}