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

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    // Giao diện quản lý sự kiện y tế
    public partial class MedicalEvent : Window
    {
        IIncidentService _incidentService;
        private User currentUser;

        public MedicalEvent(User user = null)
        {
            InitializeComponent();
            currentUser = user;
            LoadData();
        }

        // Tải dữ liệu sự kiện y tế
        public void LoadData()
        {
            _incidentService = new IncidentService();
            dgMedicalEvents.ItemsSource = null;
            dgMedicalEvents.ItemsSource = _incidentService.GetAllIncidents();
        }

        // Xử lý sự kiện thêm sự kiện y tế mới
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new MedicalEventAddAndEdit();
            if (addWindow.ShowDialog() == true)
            {
                LoadData();
            }
        }

        // Xử lý sự kiện sửa sự kiện y tế
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicalEvents.SelectedItem is Incident selectedIncident)
            {
                var editWindow = new MedicalEventAddAndEdit(selectedIncident);
                if (editWindow.ShowDialog() == true)
                {
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sự kiện y tế để sửa.");
            }
        }

        // Xử lý sự kiện xóa sự kiện y tế
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgMedicalEvents.SelectedItem is Incident selectedIncident)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa sự kiện y tế mã '{selectedIncident.IncidentId}'?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    var service = new IncidentService();
                    // Soft delete để giữ lịch sử
                    service.SoftDeleteIncident(selectedIncident.IncidentId);
                    MessageBox.Show("Đã xóa sự kiện y tế thành công!");
                    LoadData();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một sự kiện y tế để xóa.");
            }
        }

        // Xử lý sự kiện xuất Excel
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            // Chức năng xuất Excel sẽ được triển khai sau
        }

        // Xử lý sự kiện làm mới dữ liệu
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        // Xử lý sự kiện tìm kiếm
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string keyword = SearchTextBox.Text.Trim();
            if (string.IsNullOrEmpty(keyword))
            {
                LoadData();
                return;
            }
            var service = new IncidentService();
            // Lọc dữ liệu theo từ khóa tìm kiếm
            var all = service.GetAllIncidents();
            var filtered = all.FindAll(i =>
                (i.Student?.FullName != null && i.Student.FullName.ToLower().Contains(keyword.ToLower())) ||
                (i.Description != null && i.Description.ToLower().Contains(keyword.ToLower())) ||
                (i.IncidentType?.EventTypeName != null && i.IncidentType.EventTypeName.ToLower().Contains(keyword.ToLower())) ||
                (i.ReportedByNavigation?.FullName != null && i.ReportedByNavigation.FullName.ToLower().Contains(keyword.ToLower())) ||
                (i.Location != null && i.Location.ToLower().Contains(keyword.ToLower()))
            );
            dgMedicalEvents.ItemsSource = null;
            dgMedicalEvents.ItemsSource = filtered;
            RecordCountLabel.Text = $"Tổng: {filtered.Count} sự kiện";
        }

        // Quay lại dashboard
        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            NurseDashboard dashboard = new NurseDashboard(currentUser);
            dashboard.Show();
            this.Close();
        }
    }
}
