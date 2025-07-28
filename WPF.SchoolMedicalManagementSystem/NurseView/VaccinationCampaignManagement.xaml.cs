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
    public partial class VaccinationCampaignManagement : Window
    {
        private VaccinationCampaignService _campaignService;
        private User _currentUser;
        private List<VaccinationCampaign> _allCampaigns;

        public VaccinationCampaignManagement(User user)
        {
            InitializeComponent();
            _currentUser = user;
            _campaignService = new VaccinationCampaignService();
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _allCampaigns = _campaignService.GetAllVaccinationCampaigns()
                    .Where(c => c.IsActive == true)
                    .OrderByDescending(c => c.Date)
                    .ToList();

                dgCampaigns.ItemsSource = _allCampaigns;
                UpdateRecordCount();
                StatusLabel.Text = "Dữ liệu đã được tải thành công";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi tải dữ liệu";
            }
        }

        private void UpdateRecordCount()
        {
            int count = _allCampaigns?.Count ?? 0;
            RecordCountLabel.Text = $"Tổng: {count} đợt tiêm";
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
                    dgCampaigns.ItemsSource = _allCampaigns;
                    UpdateRecordCount();
                    return;
                }

                var filtered = _allCampaigns.Where(campaign =>
                    (campaign.VaccineName?.ToLower().Contains(keyword) == true) ||
                    (campaign.Description?.ToLower().Contains(keyword) == true) ||
                    (campaign.CreatedByNavigation?.FullName?.ToLower().Contains(keyword) == true)
                ).ToList();

                dgCampaigns.ItemsSource = filtered;
                RecordCountLabel.Text = $"Tổng: {filtered.Count} đợt tiêm";
                StatusLabel.Text = $"Tìm thấy {filtered.Count} kết quả cho '{txtSearch.Text}'";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi tìm kiếm";
            }
        }

        private void AddCampaignButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addDialog = new VaccinationCampaignDialog(_currentUser.UserId);
                if (addDialog.ShowDialog() == true)
                {
                    LoadData();
                    StatusLabel.Text = "Đã thêm đợt tiêm chủng thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở dialog thêm mới: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi thêm đợt tiêm";
            }
        }

        private void ViewDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (dgCampaigns.SelectedItem is VaccinationCampaign selectedCampaign)
            {
                ViewCampaignDetail(selectedCampaign);
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đợt tiêm chủng để xem chi tiết.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ViewCampaignDetailButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is VaccinationCampaign campaign)
            {
                ViewCampaignDetail(campaign);
            }
        }

        private void ViewStudentListButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is VaccinationCampaign campaign)
            {
                try
                {
                    var studentListWindow = new VaccinationCampaignStudentList(campaign, _currentUser);
                    studentListWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở danh sách học sinh: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ViewCampaignDetail(VaccinationCampaign campaign)
        {
            try
            {
                var editDialog = new VaccinationCampaignDialog(_currentUser.UserId, campaign);
                if (editDialog.ShowDialog() == true)
                {
                    LoadData();
                    StatusLabel.Text = "Đã cập nhật đợt tiêm chủng thành công";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở chi tiết đợt tiêm: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                StatusLabel.Text = "Lỗi khi xem chi tiết";
            }
        }

        private void DgCampaigns_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgCampaigns.SelectedItem is VaccinationCampaign selectedCampaign)
            {
                try
                {
                    var studentListWindow = new VaccinationCampaignStudentList(selectedCampaign, _currentUser);
                    studentListWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở danh sách học sinh: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
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

        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dashboard = new NurseDashboard(_currentUser);
                dashboard.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi quay lại dashboard: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}