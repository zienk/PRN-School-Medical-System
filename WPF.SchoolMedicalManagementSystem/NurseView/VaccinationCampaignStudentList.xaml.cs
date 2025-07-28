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
    public partial class VaccinationCampaignStudentList : Window
    {
        private VaccinationCampaign _campaign;
        private User _currentUser;
        private VaccinationRecordService _vaccinationRecordService;
        private List<VaccinationRecord> _allRecords;

        public VaccinationCampaignStudentList(VaccinationCampaign campaign, User user)
        {
            InitializeComponent();
            _campaign = campaign;
            _currentUser = user;
            _vaccinationRecordService = new VaccinationRecordService();
            
            InitializeUI();
            LoadData();
        }

        private void InitializeUI()
        {
            txtHeader.Text = $"DANH SÁCH HỌC SINH - {_campaign.VaccineName?.ToUpper()}";
            txtVaccineName.Text = _campaign.VaccineName ?? "N/A";
            txtCampaignDate.Text = _campaign.Date?.ToString("dd/MM/yyyy") ?? "N/A";
            txtStatus.Text = _campaign.Status?.StatusName ?? "N/A";
        }

        private void LoadData()
        {
            try
            {
                _allRecords = _vaccinationRecordService.GetVaccinationRecordsByCampaignId(_campaign.CampaignId)
                    .Where(r => r.IsActive == true)
                    .OrderBy(r => r.Student?.FullName)
                    .ToList();

                dgStudents.ItemsSource = _allRecords;
                UpdateStatistics();
                StatusLabel.Text = "Dữ liệu đã được tải thành công";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi tải dữ liệu";
            }
        }

        private void UpdateStatistics()
        {
            if (_allRecords == null) return;

            int total = _allRecords.Count;
            int success = _allRecords.Count(r => r.Result == "Success");
            int failed = _allRecords.Count(r => r.Result == "Failed");
            int pending = _allRecords.Count(r => string.IsNullOrEmpty(r.Result));

            txtStatistics.Text = $"Tổng: {total} | Thành công: {success} | Thất bại: {failed} | Chưa tiêm: {pending}";
            RecordCountLabel.Text = $"Tổng: {total} học sinh";
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
                    dgStudents.ItemsSource = _allRecords;
                    return;
                }

                var filtered = _allRecords.Where(record =>
                    (record.Student?.FullName?.ToLower().Contains(keyword) == true) ||
                    (record.Student?.Class?.ToLower().Contains(keyword) == true) ||
                    (record.Notes?.ToLower().Contains(keyword) == true)
                ).ToList();

                dgStudents.ItemsSource = filtered;
                StatusLabel.Text = $"Tìm thấy {filtered.Count} kết quả cho '{txtSearch.Text}'";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi tìm kiếm";
            }
        }

        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new VaccinationStudentListDialog(_campaign.CampaignId);
                if (dialog.ShowDialog() == true)
                {
                    // Tạo VaccinationRecord cho các học sinh được chọn
                    bool success = _vaccinationRecordService.CreateVaccinationRecordsForCampaign(
                        _campaign.CampaignId, 
                        dialog.SelectedStudentIds);

                    if (success)
                    {
                        MessageBox.Show($"Đã thêm {dialog.SelectedStudentIds.Count} học sinh vào danh sách tiêm chủng!", 
                                      "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                        StatusLabel.Text = "Đã thêm học sinh thành công";
                    }
                    else
                    {
                        MessageBox.Show("Không có học sinh mới nào được thêm. Có thể các học sinh đã có trong danh sách.", 
                                      "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm học sinh: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi thêm học sinh";
            }
        }

        private void RecordResultButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgStudents.SelectedItem is VaccinationRecord selectedRecord)
            {
                RecordVaccinationResult(selectedRecord);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một học sinh để ghi nhận kết quả tiêm.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void RecordResultRowButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is VaccinationRecord record)
            {
                RecordVaccinationResult(record);
            }
        }

        private void RecordVaccinationResult(VaccinationRecord record)
        {
            try
            {
                var resultDialog = new VaccinationSimpleResultDialog(record);
                if (resultDialog.ShowDialog() == true)
                {
                    LoadData();
                    StatusLabel.Text = "Đã ghi nhận kết quả tiêm chủng thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi nhận kết quả: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi ghi nhận kết quả";
            }
        }

        private void DgStudents_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgStudents.SelectedItem is VaccinationRecord selectedRecord)
            {
                RecordVaccinationResult(selectedRecord);
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                txtSearch.Text = string.Empty;
                LoadData();
                StatusLabel.Text = "Đã làm mới dữ liệu";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi làm mới dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi làm mới";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var campaignManagement = new VaccinationCampaignManagement(_currentUser);
                campaignManagement.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}