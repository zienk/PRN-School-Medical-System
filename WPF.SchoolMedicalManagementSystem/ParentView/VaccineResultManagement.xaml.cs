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
namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    /// <summary>
    /// Interaction logic for VaccineResultManagement.xaml
    /// </summary>
    public partial class VaccineResultManagement : Window
    {
        private User currentUser;
        private List<Student> students;
        private List<VaccinationRecord> vaccinationRecords;
        private Student selectedStudent;


        private IStudentService studentService = new StudentService();
        private IVaccinationRecordService vaccinationRecordService = new VaccinationRecordService();
        public VaccineResultManagement(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadStudents();

            
        }

        private void LoadStudents()
        {
            students = studentService.GetAllStudentsByUserId(currentUser.UserId);
            cmbStudents.Items.Clear();
            foreach (var student in students)
            {
                //student.Gender.GenderName = student.GenderId == 1 ? "1" : "2";
                if (student.GenderId == 1)
                {
                    student.Gender.GenderName = "👦";
                }
                else
                {
                    student.Gender.GenderName = "👧";
                }
                cmbStudents.Items.Add(student);
            }
        }

        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            ParentDashboard dashboard = new ParentDashboard(currentUser);
            dashboard.Show();
            this.Close();
        }

        private void btnHealthRecord_Click(object sender, RoutedEventArgs e)
        {
            HealthRecordManagement healthRecordManagement = new HealthRecordManagement(currentUser);
            healthRecordManagement.Show();
            this.Close();
        }

        private void btnIncidents_Click(object sender, RoutedEventArgs e)
        {
            MedicalIncidentsManagement medicalIncidents = new MedicalIncidentsManagement(currentUser);
            medicalIncidents.Show();
            this.Close();
        }

        private void cmbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudents.SelectedItem is Student selectedStudent)
            {
                // Load vaccination records for the selected student
                
                this.selectedStudent = selectedStudent;
                vaccinationRecords = vaccinationRecordService.GetAllVaccinationRecordsByStudentId(selectedStudent.StudentId);
                dgVaccinationRecords.ItemsSource = vaccinationRecords;
            }
            txtVaccinationSummary.Text = $"Total Vaccinations: {vaccinationRecords.Count}";
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudent != null)
            {
                // Test xem có gọi lại DB không
                Console.WriteLine("Refreshing student ID: " + selectedStudent.StudentId);
                MessageBox.Show(selectedStudent.StudentId.ToString());
                // Gọi lại DB
                vaccinationRecords = vaccinationRecordService.GetAllVaccinationRecordsByStudentId(selectedStudent.StudentId);
                dgVaccinationRecords.ItemsSource = null; // Xóa dữ liệu cũ
                dgVaccinationRecords.ItemsSource = vaccinationRecords;

                txtVaccinationSummary.Text = $"Total Vaccinations: {vaccinationRecords.Count}";

                // Hiện/ẩn EmptyState
                EmptyState.Visibility = vaccinationRecords.Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                dgVaccinationRecords.Visibility = vaccinationRecords.Count == 0 ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Please select a student before refreshing.", "No Student Selected", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


    }
}
