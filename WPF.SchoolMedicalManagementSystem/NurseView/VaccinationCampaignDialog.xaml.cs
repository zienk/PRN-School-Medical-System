using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for VaccinationCampaignDialog.xaml
    /// </summary>
    public partial class VaccinationCampaignDialog : Window
    {
        private readonly IVaccinationCampaignService _campaignService;
        private readonly ICampaignStatusService _statusService;
        private VaccinationCampaign _campaign;
        private bool _isEditMode = false;
        private Guid _currentUserId;

        public VaccinationCampaignDialog(Guid userId)
        {
            InitializeComponent();
            _currentUserId = userId;
            _campaignService = new VaccinationCampaignService();
            _statusService = new CampaignStatusService();
            LoadStatuses();
            btnSave.Content = "➕ Create";
        }

        public VaccinationCampaignDialog(Guid userId, VaccinationCampaign campaign) : this(userId)
        {
            _campaign = campaign;
            _isEditMode = true;
            LoadCampaignData();
            txtHeader.Text = "Edit Vaccination Campaign";
            btnSave.Content = "✏️ Update";
        }

        private void LoadStatuses()
        {
            try
            {
                var statuses = _statusService.GetAllCampaignStatuses();
                cmbStatus.ItemsSource = statuses;
                cmbStatus.DisplayMemberPath = "StatusName";
                cmbStatus.SelectedValuePath = "StatusId";
                
                // Default to first status (usually "Planned" or "Active")
                if (statuses.Count > 0)
                    cmbStatus.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading statuses: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadCampaignData()
        {
            if (_campaign != null)
            {
                txtCampaignName.Text = _campaign.VaccineName;
                dpDate.SelectedDate = _campaign.Date?.ToDateTime(TimeOnly.MinValue);
                txtDescription.Text = _campaign.Description;
                cmbStatus.SelectedValue = _campaign.StatusId;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ValidateForm())
                {
                    if (_isEditMode)
                    {
                        UpdateCampaign();
                    }
                    else
                    {
                        SaveNewCampaign();
                    }
                    
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving campaign: {ex.Message}\n{ex.InnerException?.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtCampaignName.Text))
            {
                MessageBox.Show("Campaign name is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (!dpDate.SelectedDate.HasValue)
            {
                MessageBox.Show("Date is required!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (cmbStatus.SelectedItem == null)
            {
                MessageBox.Show("Please select a status!", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void SaveNewCampaign()
        {
            var campaign = new VaccinationCampaign
            {
                VaccineName = txtCampaignName.Text,
                Date = DateOnly.FromDateTime(dpDate.SelectedDate.Value),
                Description = txtDescription.Text,
                CreatedBy = _currentUserId,
                StatusId = (int)cmbStatus.SelectedValue,
                IsActive = true
            };
            _campaignService.AddVaccinationCampaign(campaign);
            MessageBox.Show("Campaign added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateCampaign()
        {
            if (_campaign != null)
            {
                _campaign.VaccineName = txtCampaignName.Text;
                _campaign.Date = DateOnly.FromDateTime(dpDate.SelectedDate.Value);
                _campaign.Description = txtDescription.Text;
                _campaign.StatusId = (int)cmbStatus.SelectedValue;
                _campaignService.UpdateVaccinationCampaign(_campaign);
                MessageBox.Show("Campaign updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
} 