using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BusinessObjects.Entities;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for VaccinationRecordManagement.xaml
    /// </summary>
    public partial class VaccinationRecordManagement : Window
    {
        private VaccinationRecordService _vaccinationRecordService;
        private VaccinationCampaignService _vaccinationCampaignService;
        private StudentService _studentService;
        private User currentUser;
        private List<VaccinationRecord> allRecords;

        public VaccinationRecordManagement(User user = null)
        {
            InitializeComponent();
            currentUser = user;
            InitializeServices();
            LoadData();
        }

        private void InitializeServices()
        {
            _vaccinationRecordService = new VaccinationRecordService();
            _vaccinationCampaignService = new VaccinationCampaignService();
            _studentService = new StudentService();
        }

        public void LoadData()
        {
            try
            {
                LoadCampaigns();
                LoadVaccinationRecords();
                UpdateRecordCount();
                StatusLabel.Text = "Dữ liệu đã được tải thành công";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi tải dữ liệu";
            }
        }

        private void LoadCampaigns()
        {
            var campaigns = _vaccinationCampaignService.GetAllVaccinationCampaigns();
            
            // Thêm tùy chọn "Tất cả chiến dịch" vào đầu danh sách
            var allCampaignsOption = new VaccinationCampaign
            {
                CampaignId = 0,
                VaccineName = "-- Tất cả chiến dịch --"
            };
            campaigns.Insert(0, allCampaignsOption);
            
            cmbCampaigns.ItemsSource = campaigns;
            cmbCampaigns.SelectedIndex = 0;
        }

        private void LoadVaccinationRecords()
        {
            // Nếu có chiến dịch được chọn (không phải "Tất cả")
            if (cmbCampaigns.SelectedValue != null && cmbCampaigns.SelectedValue is int selectedValue && selectedValue > 0)
            {
                allRecords = _vaccinationRecordService.GetVaccinationRecordsByCampaignId(selectedValue);
            }
            else
            {
                // Lấy tất cả bản ghi từ tất cả các chiến dịch
                allRecords = _vaccinationRecordService.GetAllVaccinationRecords();
            }
            
            dgVaccinationRecords.ItemsSource = null;
            dgVaccinationRecords.ItemsSource = allRecords;
        }

        private void UpdateRecordCount()
        {
            int count = allRecords?.Count ?? 0;
            RecordCountLabel.Text = $"Tổng: {count} bản ghi";
        }

        private void CmbCampaigns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCampaigns.SelectedItem != null)
            {
                LoadVaccinationRecords();
                UpdateRecordCount();
                txtSearch.Text = string.Empty; // Clear search when changing campaign
            }
        }

        private void TxtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchButton_Click(sender, e);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim().ToLower();
                
                if (string.IsNullOrEmpty(keyword))
                {
                    LoadVaccinationRecords();
                    return;
                }

                var filtered = allRecords.Where(record =>
                    (record.Student?.FullName?.ToLower().Contains(keyword) == true) ||
                    (record.Student?.Class?.ToLower().Contains(keyword) == true) ||
                    (record.Campaign?.VaccineName?.ToLower().Contains(keyword) == true) ||
                    (record.Notes?.ToLower().Contains(keyword) == true)
                ).ToList();

                dgVaccinationRecords.ItemsSource = null;
                dgVaccinationRecords.ItemsSource = filtered;
                RecordCountLabel.Text = $"Tổng: {filtered.Count} bản ghi";
                StatusLabel.Text = $"Tìm thấy {filtered.Count} kết quả cho '{txtSearch.Text}'";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi tìm kiếm";
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedCampaign = cmbCampaigns.SelectedItem as VaccinationCampaign;
                var addDialog = new VaccinationRecordDialog(selectedCampaign);
                
                if (addDialog.ShowDialog() == true)
                {
                    LoadData();
                    StatusLabel.Text = "Đã thêm bản ghi tiêm chủng thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở dialog thêm mới: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi thêm bản ghi";
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgVaccinationRecords.SelectedItem is VaccinationRecord selectedRecord)
                {
                    var editDialog = new VaccinationRecordDialog(selectedRecord);
                    
                    if (editDialog.ShowDialog() == true)
                    {
                        LoadData();
                        StatusLabel.Text = "Đã cập nhật bản ghi tiêm chủng thành công";
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi tiêm chủng để sửa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở dialog chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi sửa bản ghi";
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgVaccinationRecords.SelectedItem is VaccinationRecord selectedRecord)
                {
                    var result = MessageBox.Show(
                        $"Bạn có chắc muốn xóa bản ghi tiêm chủng của học sinh '{selectedRecord.Student?.FullName}' trong chiến dịch '{selectedRecord.Campaign?.VaccineName}'?",
                        "Xác nhận xóa",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Warning);

                    if (result == MessageBoxResult.Yes)
                    {
                        bool success = _vaccinationRecordService.DeleteVaccinationRecord(selectedRecord.VaccinationRecordId);
                        
                        if (success)
                        {
                            MessageBox.Show("Đã xóa bản ghi tiêm chủng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                            LoadData();
                            StatusLabel.Text = "Đã xóa bản ghi thành công";
                        }
                        else
                        {
                            MessageBox.Show("Không thể xóa bản ghi tiêm chủng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            StatusLabel.Text = "Lỗi khi xóa bản ghi";
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi tiêm chủng để xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa bản ghi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi xóa bản ghi";
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                cmbCampaigns.SelectedIndex = 0;
                LoadData();
                StatusLabel.Text = "Đã làm mới dữ liệu";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi làm mới";
            }
        }

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NurseDashboard dashboard = new NurseDashboard(currentUser);
                dashboard.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại dashboard: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewDetailButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dgVaccinationRecords.SelectedItem is VaccinationRecord selectedRecord)
                {
                    var detailDialog = new VaccinationRecordDetailDialog(selectedRecord, this);
                    detailDialog.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một bản ghi tiêm chủng để xem chi tiết.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết bản ghi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi xem chi tiết";
            }
        }

        private void DgVaccinationRecords_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (dgVaccinationRecords.SelectedItem is VaccinationRecord selectedRecord)
                {
                    var detailDialog = new VaccinationRecordDetailDialog(selectedRecord, this);
                    detailDialog.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết bản ghi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi xem chi tiết";
            }
        }
    }
}