using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    public partial class HealthRecordManagement : Window
    {
        private IStudentService studentService = new StudentService();
        private IHealthRecordService healthRecordService = new HealthRecordService();

        private User user;
        private int selectedStudentId = 0;
        private List<Student> students;
        private List<HealthRecord> records;

        public HealthRecordManagement(User currentUser)
        {
            user = currentUser;
            InitializeComponent();
            InitializeData();
        }

        private void InitializeData()
        {

            //Sample student data
            records = healthRecordService.getAllMedicalRecordOfStudentByUserId(user.UserId);
            students = studentService.GetAllStudentsByUserId(user.UserId);
            MessageBox.Show(records.Count.ToString());
            // Sample health records
            

            LoadLeftPanel();
        }

        private void LoadLeftPanel()
        {
            //"👧" : "👦";
            //SelectedStudentAvatar.Background = record.Student.GenderId == 2 ?
            //    new SolidColorBrush(Color.FromRgb(255, 224, 178)) :
            //    new SolidColorBrush(Color.FromRgb(225, 245, 254));
            foreach(Student student in students)
            {
                student.HealthRecord = records.Find(r => r.StudentId == student.StudentId);
                    if (student.GenderId == 1)
                    {
                        student.Gender.GenderName = "👦";
                    }
                    else
                    {
                        student.Gender.GenderName = "👧";
                    }
                StudentListPanel.Items.Add(student);
            }
            
        }

        private void StudentCard_Click(object sender, MouseButtonEventArgs e)
        {

            if (sender is Border border && border.Tag is Student student)
            {
                //int healthRecordId = int.Parse(border.Tag.ToString());
                MessageBox.Show(border.Tag.ToString());
                SelectStudent(student);
            }
        }

        private void SelectStudent(Student student)
        {
            selectedStudentId = student.StudentId;
            var record = records.Find(s => s.StudentId == student.StudentId);

                // Update visual selection
                //UpdateStudentSelection(studentId);

                // Show the form panel
                NoStudentSelectedMessage.Visibility = Visibility.Collapsed;
                SelectedStudentInfo.Visibility = Visibility.Visible;

            // Update student info in the form
            UpdateSelectedStudentInfo(student);

            // Load health record if exists
            LoadHealthRecord(record);
        }

        private void UpdateStudentSelection(int studentId)
        {
            ////Reset all cards to default style
            //Student1Card.BorderBrush = new SolidColorBrush(Color.FromRgb(232, 234, 237));
            //Student1Card.BorderThickness = new Thickness(1);
            //Student2Card.BorderBrush = new SolidColorBrush(Color.FromRgb(232, 234, 237));
            //Student2Card.BorderThickness = new Thickness(1);

            //// Highlight selected card
            //Border selectedCard = studentId == 1 ? Student1Card : Student2Card;
            //selectedCard.BorderBrush = new SolidColorBrush(Color.FromRgb(66, 133, 244));
            //selectedCard.BorderThickness = new Thickness(2);
        }

        private void UpdateSelectedStudentInfo(Student student)
        {
            SelectedStudentName.Text = student.FullName;
            SelectedStudentInfo_Details.Text = $"Student ID: {student.StudentId}";

            // Update avatar
            SelectedStudentEmoji.Text = student.GenderId == 2 ? "👧" : "👦";
            SelectedStudentAvatar.Background = student.GenderId == 2 ?
                new SolidColorBrush(Color.FromRgb(255, 224, 178)) :
                new SolidColorBrush(Color.FromRgb(225, 245, 254));

            // Update record status
            //if (record.IsActive == true)
            //{
            //    RecordStatusText.Text = "Has Record";
            //    RecordStatusBorder.Background = new SolidColorBrush(Color.FromRgb(232, 245, 232));
            //    RecordStatusText.Foreground = new SolidColorBrush(Color.FromRgb(46, 125, 50));
            //}
            //else
            //{
            //    RecordStatusText.Text = "No Record";
            //    RecordStatusBorder.Background = new SolidColorBrush(Color.FromRgb(255, 243, 224));
            //    RecordStatusText.Foreground = new SolidColorBrush(Color.FromRgb(230, 81, 0));
            //}
        }

        private void LoadHealthRecord(HealthRecord record)
        {
            //MessageBox.Show(record.Height.ToString()+"ssss" + record.Weight.ToString());

            if (record != null)
            {
                //var record = healthRecords[studentId];
                txtHeight.Text = record.Height.ToString();
                txtWeight.Text = record.Weight.ToString();
                txtChronicDiseases.Text = record.ChronicDiseases;
                txtAllergies.Text = record.Allergies;
                txtNotes.Text = record.Notes;
            }
            else
            {
                // Clear form for new record
                ClearForm();
            }
        }

        private void ClearForm()
        {
            txtHeight.Clear();
            txtWeight.Clear();
            txtChronicDiseases.Clear();
            txtAllergies.Clear();
            txtNotes.Clear();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (selectedStudentId == 0)
            {
                MessageBox.Show("Please select a student first.", "No Student Selected",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            MessageBox.Show(selectedStudentId + "fffff");

            //// Validate required fields
            if (string.IsNullOrWhiteSpace(txtHeight.Text) || string.IsNullOrWhiteSpace(txtWeight.Text))
            {
                MessageBox.Show("Height and Weight are required fields.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //// Validate numeric fields
            if (!float.TryParse(txtHeight.Text, out float height) || height <= 0)
            {
                MessageBox.Show("Please enter a valid height in centimeters.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!float.TryParse(txtWeight.Text, out float weight) || weight <= 0)
            {
                MessageBox.Show("Please enter a valid weight in kilograms.", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //// Save the record
            var record = new HealthRecord
            {
                StudentId = selectedStudentId,
                Height = decimal.Parse(txtHeight.Text),
                Weight = decimal.Parse(txtWeight.Text),
                ChronicDiseases = txtChronicDiseases.Text,
                Allergies = txtAllergies.Text,
                Notes = txtNotes.Text
            };

            //healthRecords[selectedStudentId] = record;

            //// Update student record status
            //var student = students.Find(s => s.Id == selectedStudentId);
            //if (student != null)
            //{
            //    student.HasRecord = true;
            //    //UpdateSelectedStudentInfo(student);
            //    UpdateStudentCardStatus(selectedStudentId, true);
            //}

            HealthRecord recordUpdate = healthRecordService.UpdateHealthRecord(record);
            //records.Add(recordUpdate);
            if(recordUpdate != null)
            {
            records.Remove(record);
            records.Add(recordUpdate);
                MessageBox.Show("Health record saved successfully!", "Success",
                                MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to save health record. Please try again.", "Error",
                                MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void UpdateStudentCardStatus(int studentId, bool hasRecord)
        {
            // This method would update the student card status in the UI
            // For now, we'll just refresh the student selection
            //SelectStudent(studentId);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear all form fields?", "Clear Form",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                ClearForm();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to parent dashboard
            var parentDashboard = new ParentDashboard(user);
            parentDashboard.Show();
            this.Close();
        }

        
    }

    
}