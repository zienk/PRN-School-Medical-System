using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessObjects.Entities;
using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    public partial class HealthCheckupResultManagement : Window
    {
        private List<Student> students;
        private List<HealthCheckupResult> healthCheckupResults;

        private User currentUser;
        private StudentService _studentService = new StudentService();
        private HealthCheckupResultService _healthCheckupResultService = new HealthCheckupResultService();

        public HealthCheckupResultManagement(User user)
        {
            InitializeComponent();
            currentUser = user;
            LoadStudents();
        }

        private void LoadStudents()
        {
            students = _studentService.GetAllStudentsByUserId(currentUser.UserId);

            foreach (Student student in students)
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


        private void cmbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudents.SelectedItem is Student selectedStudent)
            {
                //MessageBox.Show($"Selected Student: {selectedStudent.FullName}");
                healthCheckupResults = _healthCheckupResultService.getAllHealthCheckupResultByStudentId(selectedStudent.StudentId);
                DisplayCheckupResults(healthCheckupResults);

                txtNoResults.Visibility = Visibility.Collapsed;
            }
        }

        private void DisplayCheckupResults(List<HealthCheckupResult> healthCheckupResults)
        {
            stackCheckupResults.Children.Clear();

            //healthCheckupResults = _healthCheckupResultService.getAllHealthCheckupResultByStudentId(studentId);

            if (healthCheckupResults.Any())
            {
                foreach (var checkup in healthCheckupResults)
                {
                    var checkupCard = CreateCheckupCard(checkup);
                    stackCheckupResults.Children.Add(checkupCard);
                }
            }
            else
            {
                txtNoResults.Text = "No health checkup results found for this student.";
                txtNoResults.Visibility = Visibility.Visible;
                stackCheckupResults.Children.Add(txtNoResults);
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            string filterText = txtTenChuongTrinh.Text.Trim().ToLower();
            if(!string.IsNullOrEmpty(filterText))
            {
                // If filter is empty, show all results
                healthCheckupResults = healthCheckupResults.Where(x => x.Checkup.CheckupName.ToLower().Contains(filterText)).ToList();
                DisplayCheckupResults(healthCheckupResults);
                return;
            }
            //DisplayCheckupResults(healthCheckupResults);
        }

        private Border CreateCheckupCard(HealthCheckupResult checkup)
        {
            var card = new Border
            {
                Style = (Style)FindResource("CheckupItemStyle")
            };

            var mainGrid = new Grid();
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Header
            var headerGrid = new Grid();
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            headerGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            var titleStack = new StackPanel();
            var titleText = new TextBlock
            {
                Text = $"🩺 {checkup.Checkup.CheckupName}",
                FontSize = 18,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(32, 33, 36)),
                Margin = new Thickness(0, 0, 0, 5)
            };

            var dateText = new TextBlock
            {
                Text = checkup.CheckupTime?.ToString("MMMM-dd-yyyy"),
                FontSize = 14,
                Foreground = new SolidColorBrush(Color.FromRgb(95, 99, 104))
            };

            titleStack.Children.Add(titleText);
            titleStack.Children.Add(dateText);

            // Status
            var statusBorder = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(232, 245, 233)),
                CornerRadius = new CornerRadius(15),
                //Padding = new Thickness(12, 6)
            };

            //var statusText = new TextBlock
            //{
            //    Text = "✅ " + checkup.IsActive,
            //    FontSize = 12,
            //    FontWeight = FontWeights.SemiBold,
            //    Foreground = new SolidColorBrush(Color.FromRgb(46, 125, 50))
            //};

            //statusBorder.Child = statusText;

            Grid.SetColumn(titleStack, 0);
            Grid.SetColumn(statusBorder, 1);
            headerGrid.Children.Add(titleStack);
            headerGrid.Children.Add(statusBorder);

            // Separator
            var separator = new Border
            {
                Height = 1,
                Background = new SolidColorBrush(Color.FromRgb(232, 234, 237)),
                Margin = new Thickness(0, 15, 0, 15)
            };

            // Details Grid
            var detailsGrid = new Grid();
            detailsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            detailsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            detailsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            // Column 1
            var column1 = new StackPanel { Margin = new Thickness(0, 0, 15, 0) };
            column1.Children.Add(CreateDetailCard("📏 Height", checkup.Height.ToString()));
            column1.Children.Add(CreateDetailCard("⚖️ Weight", checkup.Weight.ToString()));
            column1.Children.Add(CreateDetailCard("📊 BMI", checkup.Bmi.ToString()));

            // Column 2
            var column2 = new StackPanel { Margin = new Thickness(0, 0, 15, 0) };
            column2.Children.Add(CreateDetailCard("👁️ Vision", checkup.Vision));
            column2.Children.Add(CreateDetailCard("🦷 Dental Status", checkup.DentalStatus));
            column2.Children.Add(CreateDetailCard("💗 Heart Rate", checkup.HeartRate.ToString()));

            // Column 3
            var column3 = new StackPanel();
            column3.Children.Add(CreateDetailCard("🩸 Blood Pressure", checkup.BloodPressure));
            column3.Children.Add(CreateDetailCard("🏥 General Condition", checkup.GeneralCondition));

            Grid.SetColumn(column1, 0);
            Grid.SetColumn(column2, 1);
            Grid.SetColumn(column3, 2);
            detailsGrid.Children.Add(column1);
            detailsGrid.Children.Add(column2);
            detailsGrid.Children.Add(column3);

            // Notes section
            Border notesCard = null;
            if (!string.IsNullOrEmpty(checkup.Notes))
            {
                notesCard = new Border
                {
                    Background = new SolidColorBrush(Color.FromRgb(255, 248, 225)),
                    CornerRadius = new CornerRadius(8),
                    Padding = new Thickness(15),
                    Margin = new Thickness(0, 15, 0, 0)
                };

                var notesStack = new StackPanel();
                var notesHeader = new TextBlock
                {
                    Text = "📝 Doctor's Notes",
                    FontSize = 14,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = new SolidColorBrush(Color.FromRgb(229, 81, 0)),
                    Margin = new Thickness(0, 0, 0, 8)
                };

                var notesText = new TextBlock
                {
                    Text = checkup.Notes,
                    FontSize = 13,
                    Foreground = new SolidColorBrush(Color.FromRgb(95, 99, 104)),
                    TextWrapping = TextWrapping.Wrap
                };

                notesStack.Children.Add(notesHeader);
                notesStack.Children.Add(notesText);
                notesCard.Child = notesStack;
            }

            Grid.SetRow(headerGrid, 0);
            Grid.SetRow(separator, 1);
            Grid.SetRow(detailsGrid, 2);

            mainGrid.Children.Add(headerGrid);
            mainGrid.Children.Add(separator);
            mainGrid.Children.Add(detailsGrid);

            if (notesCard != null)
            {
                mainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                Grid.SetRow(notesCard, 3);
                mainGrid.Children.Add(notesCard);
            }

            card.Child = mainGrid;
            return card;
        }

        private Border CreateDetailCard(string label, string value)
        {
            var card = new Border
            {
                Style = (Style)FindResource("DetailCardStyle")
            };

            var stack = new StackPanel();

            var labelText = new TextBlock
            {
                Text = label,
                Style = (Style)FindResource("LabelStyle")
            };

            var valueText = new TextBlock
            {
                Text = value,
                Style = (Style)FindResource("ValueStyle")
            };

            stack.Children.Add(labelText);
            stack.Children.Add(valueText);
            card.Child = stack;
            return card;
        }

        private void btnHealthRecord_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to Parent Dashboard
            HealthRecordManagement healthRecordManagement = new HealthRecordManagement(currentUser);
            healthRecordManagement.Show();
            this.Close();

        }

        private void btnVaccinations_Click(object sender, RoutedEventArgs e)
        {
            VaccineResultManagement vaccineResultManagement = new VaccineResultManagement(currentUser);
            vaccineResultManagement.Show();
            this.Close();
        }

        private void btnIncidents_Click(object sender, RoutedEventArgs e)
        {
            HealthCheckupResultManagement healthCheckupManagement = new HealthCheckupResultManagement(currentUser);
            healthCheckupManagement.Show();
            this.Close();
        }

        
    }

   

}