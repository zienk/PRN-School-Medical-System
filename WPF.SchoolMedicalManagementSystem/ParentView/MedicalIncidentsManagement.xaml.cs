using Services.Interfaces;
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
using Services.Implementations;
using Services.Interfaces;
using BusinessObjects.Entities;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    /// <summary>
    /// Interaction logic for MedicalIncidents.xaml
    /// </summary>
    public partial class MedicalIncidentsManagement : Window
    {
        private User user; // Store the user information
        private List<Incident> incidents; // Declare as a field

        private IIncidentService incidentService = new IncidentService();
        private IStudentService studentService = new StudentService();
        public MedicalIncidentsManagement(User currentUser)
        {
            user = currentUser;
            InitializeComponent();
            LoadInitialData();
        }

        #region Data Loading Methods

        /// <summary>
        /// Load initial data when window opens
        /// </summary>
        private void LoadInitialData()
        {
            // TODO: Load initial data
            List<Student> students = studentService.GetAllStudentsByUserId(user.UserId);

            //MessageBox.Show(user.UserId.ToString(), "User Email", MessageBoxButton.OK, MessageBoxImage.Information);
            incidents = incidentService.GetAllIncidentsByUserId(students);

            LoadMedicalIncidents();
            LoadStudentFilter();
            LoadSeverityFilter();

            txtRecordCount.Text = incidents.Count.ToString();
        }

        /// <summary>
        /// Load medical incidents data
        /// </summary>
        private void LoadMedicalIncidents()
        {
            // TODO: Load medical incidents from database
            
            dgMedicalIncidents.ItemsSource = incidents;
        }

        /// <summary>
        /// Load student filter data
        /// </summary>
        private void LoadStudentFilter()
        {
            // TODO: Load students for filter dropdown
            foreach (Incident incident in incidents)
            {
                if (!cmbStudentFilter.Items.Contains(incident.Student.FullName))
                {
                    cmbStudentFilter.Items.Add(incident.Student.FullName);
                }
            }
        }

        /// <summary>
        /// Load severity filter data
        /// </summary>
        private void LoadSeverityFilter()
        {
            // TODO: Load severity levels for filter dropdown
            foreach (Incident incident in incidents)
            {
                if (!cmbSeverityFilter.Items.Contains(incident.Severity.SeverityName))
                {
                    cmbSeverityFilter.Items.Add(incident.Severity.SeverityName);
                }
            }
        }

        #endregion

        #region Filter Methods

        /// <summary>
        /// Apply filters to medical incidents data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Apply selected filters
            IEnumerable<Incident> filteredIncidents = incidents;

            if(cmbStudentFilter.SelectedItem != null)
            {
                string selectedStudent = cmbStudentFilter.SelectedItem.ToString();
                filteredIncidents = filteredIncidents.Where(i => i.Student.FullName == selectedStudent);
            }

            if(cmbSeverityFilter.SelectedItem != null)
            {
                filteredIncidents = filteredIncidents.Where(i => i.Severity.SeverityName == cmbSeverityFilter.SelectedItem.ToString());
            }

            if(dpDateFilter.SelectedDate != null)
            {
                DateTime selectedDate = dpDateFilter.SelectedDate.Value;
                filteredIncidents = filteredIncidents.Where(i => i.IncidentDate.Date == selectedDate.Date);
            }

            dgMedicalIncidents.ItemsSource = filteredIncidents.ToList();
            txtRecordCount.Text = filteredIncidents.Count().ToString();

        }

        /// <summary>
        /// Filter data based on selected criteria
        /// </summary>
        private void ApplyFilters()
        {
            // TODO: Apply filters logic
        }

        /// <summary>
        /// Clear all filters
        /// </summary>
        private void ClearFilters()
        {
            // TODO: Clear all filter selections
        }

        #endregion

        #region Incident Actions

        /// <summary>
        /// View incident details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewIncident_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Open incident view dialog
        }

        /// <summary>
        /// Show incident details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIncidentDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as FrameworkElement;
            if (button?.DataContext is Incident incident)
            {
                var detailsWindow = new IncidentDetailsWindow(incident);
                detailsWindow.Owner = this;
                detailsWindow.ShowDialog();
            }
        }

        /// <summary>
        /// Open incident details window
        /// </summary>
        /// <param name="incidentId"></param>
        private void OpenIncidentDetails(int incidentId)
        {
            // TODO: Open incident details window with specific incident ID
        }

        #endregion

        #region Pagination Methods

        /// <summary>
        /// Go to previous page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to previous page
        }

        /// <summary>
        /// Go to next page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to next page
        }

        /// <summary>
        /// Update pagination info
        /// </summary>
        private void UpdatePaginationInfo()
        {
            // TODO: Update page information display
        }

        /// <summary>
        /// Load specific page of data
        /// </summary>
        /// <param name="pageNumber"></param>
        private void LoadPage(int pageNumber)
        {
            // TODO: Load specific page of incidents
        }

        #endregion

        #region Navigation Methods

        /// <summary>
        /// Navigate to Health Record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHealthRecord_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Health Record window
        }

        /// <summary>
        /// Navigate to Account Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAccountInfo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Account Info window
        }

        /// <summary>
        /// Navigate to Student Info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStudentInfo_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Student Info window
        }

        /// <summary>
        /// Navigate to Health Checkups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnHealthCheckups_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Health Checkups window
        }

        /// <summary>
        /// Navigate to Vaccinations
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVaccinations_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Navigate to Vaccinations window
        }

        #endregion

        #region Utility Methods

        /// <summary>
        /// Update record count display
        /// </summary>
        private void UpdateRecordCount()
        {
            // TODO: Update record count text
        }

        /// <summary>
        /// Refresh data grid
        /// </summary>
        private void RefreshDataGrid()
        {
            // TODO: Refresh the data grid display
        }

        /// <summary>
        /// Show message to user
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        private void ShowMessage(string message, string title = "Information")
        {
            // TODO: Show message dialog
        }

        /// <summary>
        /// Show error message to user
        /// </summary>
        /// <param name="message"></param>
        private void ShowError(string message)
        {
            // TODO: Show error message dialog
        }

        /// <summary>
        /// Validate form data
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            // TODO: Validate form data
            return true;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Handle window loaded event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // TODO: Handle window loaded
        }

        /// <summary>
        /// Handle data grid selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgMedicalIncidents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Handle selection change
        }

        /// <summary>
        /// Handle combo box selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Handle filter combo box changes
        }

        /// <summary>
        /// Handle date picker value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO: Handle date filter changes
        }

        #endregion

        private void btnResetFilter_Click(object sender, RoutedEventArgs e)
        {
            cmbSeverityFilter.SelectedItem = null;
            cmbStudentFilter.SelectedItem = null;
            dpDateFilter.SelectedDate = null;
            dgMedicalIncidents.ItemsSource = incidents; // Reset to original data
            txtRecordCount.Text = incidents.Count.ToString();
        }


        private void btnVaccinations_Click_1(object sender, RoutedEventArgs e)
        {
            VaccineResultManagement vaccineResultManagement = new VaccineResultManagement(user);
            vaccineResultManagement.Show();
            this.Close();
        }

        private void btnHealthCheckups_Click_1(object sender, RoutedEventArgs e)
        {
            HealthCheckupResultManagement healthCheckupResultManagement = new HealthCheckupResultManagement(user);
            healthCheckupResultManagement.Show();
            this.Close();
        }

        private void btnHealthRecord_Click_1(object sender, RoutedEventArgs e)
        {
            HealthRecordManagement healthRecordManagement = new HealthRecordManagement(user);
            healthRecordManagement.Show();
            this.Close();
        }
    }
}