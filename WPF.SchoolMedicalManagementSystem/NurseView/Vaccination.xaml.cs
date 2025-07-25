﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for Vaccination.xaml
    /// </summary>
    public partial class Vaccination : Window
    {
        private readonly IVaccinationCampaignService _campaignService;
        private readonly ICampaignStatusService _statusService;
        private List<VaccinationCampaign> _campaigns = new List<VaccinationCampaign>();
        private Guid _currentUserId;
        private User _currentUser;

        public Vaccination(Guid userId)
        {
            InitializeComponent();
            _currentUserId = userId;
            _campaignService = new VaccinationCampaignService();
            _statusService = new CampaignStatusService();
            
            // Get current user info
            var userService = new UserService();
            _currentUser = userService.GetUserById(_currentUserId);
            
            LoadData();
            SetupEventHandlers();
        }

        private void SetupEventHandlers()
        {
            btnAddNew.Click += BtnAddNew_Click;
            txtSearch.TextChanged += TxtSearch_TextChanged;
        }

        private void LoadData()
        {
            try
            {
                _campaigns = _campaignService.GetAllVaccinationCampaigns();
                dgVaccinations.ItemsSource = _campaigns;
                RecordCountLabel.Text = $"Tổng: {_campaigns.Count} chiến dịch";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading campaigns: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            // Mở dialog thêm mới chiến dịch
            VaccinationCampaignDialog dialog = new VaccinationCampaignDialog(_currentUserId);
            if (dialog.ShowDialog() == true)
            {
                // Nếu dialog trả về true (đã lưu), reload dữ liệu
                LoadData();
            }
        }

        private void BtnRefresh_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            txtSearch.Text = string.Empty;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchCampaigns();
        }

        private void SearchCampaigns()
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                dgVaccinations.ItemsSource = _campaigns;
                RecordCountLabel.Text = $"Tổng: {_campaigns.Count} chiến dịch";
            }
            else
            {
                var searchResults = _campaignService.SearchVaccinationCampaigns(txtSearch.Text);
                dgVaccinations.ItemsSource = searchResults;
                RecordCountLabel.Text = $"Tổng: {searchResults.Count} chiến dịch";
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is VaccinationCampaign campaign)
            {
                // Mở dialog chỉnh sửa với campaign được chọn
                VaccinationCampaignDialog dialog = new VaccinationCampaignDialog(_currentUserId, campaign);
                if (dialog.ShowDialog() == true)
                {
                    // Nếu dialog trả về true (đã lưu), reload dữ liệu
                    LoadData();
                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is VaccinationCampaign campaign)
            {
                var result = MessageBox.Show($"Are you sure you want to delete the campaign '{campaign.VaccineName}'?", 
                    "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        _campaignService.DeleteVaccinationCampaign(campaign.CampaignId);
                        MessageBox.Show("Campaign deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting campaign: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnManageHealthCheckups_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Điều hướng sang HealthCheckups nếu cần
        }

        private void btnManageVaccinations_Click(object sender, RoutedEventArgs e)
        {
            // Có thể để trống hoặc reload lại trang hiện tại
        }

        private void btnManageIncidents_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Điều hướng sang MedicalIncidents nếu cần
        }
        
        private void BackToDashboard_Click(object sender, RoutedEventArgs e)
        {
            NurseDashboard dashboard = new NurseDashboard(_currentUser);
            dashboard.Show();
            this.Close();
        }

        private void btnManageRecords_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VaccinationRecordManagement recordsWindow = new VaccinationRecordManagement(_currentUser);
                recordsWindow.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở quản lý ghi nhận tiêm chủng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
