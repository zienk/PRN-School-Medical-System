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
using WPF.SchoolMedicalManagementSystem.NurseView;

namespace WPF.SchoolMedicalManagementSystem
{
    /// <summary>
    /// Interaction logic for StudentRecordManagement.xaml
    /// </summary>
    public partial class StudentRecordManagement : Window
    {
        private IHealthRecordService _healthRecordService;
        private User currentUser;

        public StudentRecordManagement(User user = null)
        {
            InitializeComponent();
            currentUser = user;
            LoadData();
        }

        public void LoadData()
        {
            _healthRecordService = new HealthRecordService();
            dgListStudent.ItemsSource = null;
            dgListStudent.ItemsSource = _healthRecordService.GetAllHealthRecord();
            RecordCountLabel.Text = $"Tổng: {_healthRecordService.GetAllHealthRecord().Count} bản ghi";
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(sender, e);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }

            var allRecords = _healthRecordService.GetAllHealthRecord();
            var filtered = allRecords.Where(record => 
                (record.Student?.FullName?.ToLower().Contains(keyword) ?? false) ||
                (record.Student?.Class?.ToLower().Contains(keyword) ?? false) ||
                (record.ChronicDiseases?.ToLower().Contains(keyword) ?? false) ||
                (record.Allergies?.ToLower().Contains(keyword) ?? false) ||
                (record.Notes?.ToLower().Contains(keyword) ?? false)
            ).ToList();

            dgListStudent.ItemsSource = null;
            dgListStudent.ItemsSource = filtered;
            RecordCountLabel.Text = $"Tổng: {filtered.Count} bản ghi";
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = string.Empty;
            LoadData();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgListStudent.SelectedItem is HealthRecord selectedRecord)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa hồ sơ sức khỏe của học sinh '{selectedRecord.Student?.FullName}'?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    var service = new HealthRecordService();
                    // Nếu có hàm xóa cứng:
                    // service.DeleteHealthRecord(selectedRecord.HealthRecordId);
                    // Nếu chỉ soft delete:
                    selectedRecord.IsActive = false;
                    service.UpdateHealthRecord(selectedRecord);
                    MessageBox.Show("Đã xóa hồ sơ sức khỏe thành công!");
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ sức khỏe để xóa.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgListStudent.SelectedItem is HealthRecord selectedRecord)
            {
                var editWindow = new HealthRecordAddAndEdit(selectedRecord);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một hồ sơ sức khỏe để sửa.");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addHealthRecordWindow = new HealthRecordAddAndEdit();
            addHealthRecordWindow.ShowDialog();
            LoadData();
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            NurseDashboard dashboard = new NurseDashboard(currentUser);
            dashboard.Show();
            this.Close();
        }
    }
}
