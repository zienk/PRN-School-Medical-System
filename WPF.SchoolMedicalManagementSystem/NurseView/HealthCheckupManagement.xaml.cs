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
using WPF.SchoolMedicalManagementSystem.ManagerView;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    public partial class HealthCheckupManagement : Window
    {
        private User currentUser;
        private PrnEduHealthContext _context = new PrnEduHealthContext();

        private IHealthCheckupService _healthCheckupService = new HealthCheckupService();
        private IStudentService _studentService = new StudentService();
        private IHealthCheckupResultService _healthCheckupResultService = new HealthCheckupResultService();

        private bool _isEditMode = false;
        private HealthCheckup _editingCheckupProgram;
        private List<HealthCheckupResult> allHealthResults = new List<HealthCheckupResult>();

        public HealthCheckupManagement(User user)
        {
            currentUser = user;
            InitializeComponent();
            LoadCheckupPrograms();
        }

        private void LoadCheckupPrograms()
        {
            dgCheckupPrograms.ItemsSource = null;
            dgCheckupPrograms.ItemsSource = _healthCheckupService.GetAllHealthCheckups();
        }

        // Navigation Events
        private void btnDashboard_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new NurseDashboard(currentUser);
            dashboard.Show();
            this.Close();
        }

        private void btnManageVaccinations_Click(object sender, RoutedEventArgs e)
        {
            var vaccinationWindow = new Vaccination(currentUser.UserId);
            vaccinationWindow.Show();
            this.Close();
        }

        private void btnManageIncidents_Click(object sender, RoutedEventArgs e)
        {
            var medicalEventWindow = new MedicalEvent(currentUser);
            medicalEventWindow.Show();
            this.Close();
        }

        private void btnManageHealthRecords_Click(object sender, RoutedEventArgs e)
        {
            var healthRecordsWindow = new StudentRecordManagement(currentUser);
            healthRecordsWindow.Show();
            this.Close();
        }

        // Checkup Program Management Events
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
                    if (status == 3)
                    {
                        MessageBox.Show("Cannot delete the Program is Completed", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (status == 4)
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
                    CreatedBy = currentUser.UserId,
                    StatusId = 1,
                    IsActive = true
                };

                _healthCheckupService.AddHealthCheckup(newProgram);
                MessageBox.Show("Checkup program added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            popupOverlay.Visibility = Visibility.Collapsed;
            LoadCheckupPrograms();
        }

        private void btnPopupCancel_Click(object sender, RoutedEventArgs e)
        {
            popupOverlay.Visibility = Visibility.Collapsed;
        }

        private void btnCreateResultCheckup_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabCreateResults;
            allHealthResults = new List<HealthCheckupResult>();
            var button = sender as Button;
            if (button?.Tag is HealthCheckup checkupProgram)
            {
                // Get all students
                List<Student> students = _studentService.GetAllStudents();
                foreach (var student in students)
                {
                    // Create a new health checkup result for each student
                    var result = new HealthCheckupResult
                    {
                        CheckupId = checkupProgram.CheckupId,
                        StudentId = student.StudentId,
                        Height = 0,
                        Weight = 0,
                        Vision = "10/10",
                        DentalStatus = "Good",
                        BloodPressure = "120/80",
                        HeartRate = 70,
                        GeneralCondition = "Healthy",
                        Notes = "",
                        CheckupTime = DateTime.Now,
                    };
                    allHealthResults.Add(result);
                }

                allHealthResults = _healthCheckupResultService.CreateHealthCheckupResultByHealthCheckupId(allHealthResults);

                LoadCheckupPrograms();
                checkupProgram.StatusId = 3;
                _healthCheckupService.UpdateStatus(checkupProgram);

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
            if (dgHealthResults.ItemsSource is IEnumerable<HealthCheckupResult> results)
            {
                foreach (var item in results)
                {
                    _healthCheckupResultService.UpdateHealthCheckupResult(item);
                }
                MessageBox.Show("Health checkup results saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No results to save.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TxtSearchHealthResults_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearchHealthResults.Text.Trim().ToLower();
            
            if (dgHealthResults.ItemsSource == null) return;

            if (string.IsNullOrEmpty(searchText))
            {
                if (allHealthResults.Count > 0)
                {
                    dgHealthResults.ItemsSource = allHealthResults;
                }
                return;
            }

            var dataItems = dgHealthResults.ItemsSource as IEnumerable<HealthCheckupResult>;
            if (dataItems != null)
            {
                var filtered = dataItems.Where(r =>
                    r.StudentId.ToString().ToLower().Contains(searchText) ||
                    (r.Student != null && !string.IsNullOrEmpty(r.Student.FullName) && 
                     r.Student.FullName.ToLower().Contains(searchText))
                ).ToList();

                dgHealthResults.ItemsSource = filtered;
            }
        }
    }
}