using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BusinessObjects.Entities;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    public partial class HealthCheckupResultManagement : Window
    {
        private ObservableCollection<StudentModel> students;
        private ObservableCollection<HealthCheckupModel> checkupResults;
        private User currentUser;

        public HealthCheckupResultManagement(User user)
        {
            InitializeComponent();
            LoadStudents();
            LoadSampleData();
            currentUser = user;
        }

        private void LoadStudents()
        {
            students = new ObservableCollection<StudentModel>
            {
                new StudentModel { Id = 1, Name = "Emma Johnson", GradeAge = "Grade 5 • Age 10", Avatar = "👧" },
                new StudentModel { Id = 2, Name = "Michael Johnson", GradeAge = "Grade 3 • Age 8", Avatar = "👦" }
            };

            cmbStudents.ItemsSource = students;
        }

        private void LoadSampleData()
        {
            checkupResults = new ObservableCollection<HealthCheckupModel>
            {
                new HealthCheckupModel
                {
                    StudentId = 1,
                    CheckupName = "Annual Health Checkup",
                    CheckupTime = DateTime.Now.AddDays(-30),
                    Height = "140 cm",
                    Weight = "35 kg",
                    BMI = "17.9 (Normal)",
                    Vision = "20/20 (Excellent)",
                    DentalStatus = "Good - No cavities",
                    BloodPressure = "95/60 mmHg",
                    HeartRate = "85 bpm",
                    GeneralCondition = "Excellent",
                    Notes = "Student is in excellent health. Continue regular exercise and balanced diet.",
                    Status = "Completed"
                },
                new HealthCheckupModel
                {
                    StudentId = 1,
                    CheckupName = "Mid-Year Health Screening",
                    CheckupTime = DateTime.Now.AddDays(-120),
                    Height = "138 cm",
                    Weight = "33 kg",
                    BMI = "17.3 (Normal)",
                    Vision = "20/20 (Excellent)",
                    DentalStatus = "Good - Regular cleaning recommended",
                    BloodPressure = "92/58 mmHg",
                    HeartRate = "82 bpm",
                    GeneralCondition = "Good",
                    Notes = "Overall health is good. Minor recommendation for dental hygiene improvement.",
                    Status = "Completed"
                },
                new HealthCheckupModel
                {
                    StudentId = 2,
                    CheckupName = "Annual Health Checkup",
                    CheckupTime = DateTime.Now.AddDays(-25),
                    Height = "125 cm",
                    Weight = "28 kg",
                    BMI = "17.9 (Normal)",
                    Vision = "20/25 (Good)",
                    DentalStatus = "Good - One filling needed",
                    BloodPressure = "88/55 mmHg",
                    HeartRate = "90 bpm",
                    GeneralCondition = "Good",
                    Notes = "Student shows good overall health. Dental appointment scheduled for filling.",
                    Status = "Completed"
                },
                new HealthCheckupModel
                {
                    StudentId = 2,
                    CheckupName = "Sports Physical Exam",
                    CheckupTime = DateTime.Now.AddDays(-90),
                    Height = "123 cm",
                    Weight = "26 kg",
                    BMI = "17.2 (Normal)",
                    Vision = "20/25 (Good)",
                    DentalStatus = "Good",
                    BloodPressure = "85/52 mmHg",
                    HeartRate = "88 bpm",
                    GeneralCondition = "Excellent",
                    Notes = "Cleared for all sports activities. Excellent cardiovascular health.",
                    Status = "Completed"
                }
            };
        }

        private void cmbStudents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudents.SelectedItem is StudentModel selectedStudent)
            {
                DisplayCheckupResults(selectedStudent.Id);
                txtNoResults.Visibility = Visibility.Collapsed;
            }
        }

        private void DisplayCheckupResults(int studentId)
        {
            stackCheckupResults.Children.Clear();

            var studentCheckups = checkupResults.Where(c => c.StudentId == studentId).OrderByDescending(c => c.CheckupTime).ToList();

            if (studentCheckups.Any())
            {
                foreach (var checkup in studentCheckups)
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

        private Border CreateCheckupCard(HealthCheckupModel checkup)
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
                Text = $"🩺 {checkup.CheckupName}",
                FontSize = 18,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(32, 33, 36)),
                Margin = new Thickness(0, 0, 0, 5)
            };

            var dateText = new TextBlock
            {
                Text = checkup.CheckupTime.ToString("MMMM dd, yyyy"),
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

            var statusText = new TextBlock
            {
                Text = "✅ " + checkup.Status,
                FontSize = 12,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Color.FromRgb(46, 125, 50))
            };

            statusBorder.Child = statusText;

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
            column1.Children.Add(CreateDetailCard("📏 Height", checkup.Height));
            column1.Children.Add(CreateDetailCard("⚖️ Weight", checkup.Weight));
            column1.Children.Add(CreateDetailCard("📊 BMI", checkup.BMI));

            // Column 2
            var column2 = new StackPanel { Margin = new Thickness(0, 0, 15, 0) };
            column2.Children.Add(CreateDetailCard("👁️ Vision", checkup.Vision));
            column2.Children.Add(CreateDetailCard("🦷 Dental Status", checkup.DentalStatus));
            column2.Children.Add(CreateDetailCard("💗 Heart Rate", checkup.HeartRate));

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

    // Data Models
    public class StudentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GradeAge { get; set; }
        public string Avatar { get; set; }
    }

    public class HealthCheckupModel
    {
        public int StudentId { get; set; }
        public string CheckupName { get; set; }
        public DateTime CheckupTime { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BMI { get; set; }
        public string Vision { get; set; }
        public string DentalStatus { get; set; }
        public string BloodPressure { get; set; }
        public string HeartRate { get; set; }
        public string GeneralCondition { get; set; }
        public string Notes { get; set; }
        public string Status { get; set; }
    }
}