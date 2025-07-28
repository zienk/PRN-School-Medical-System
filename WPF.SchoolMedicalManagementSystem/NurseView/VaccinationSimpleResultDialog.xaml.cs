using System;
using System.Windows;
using BusinessObjects.Entities;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    public partial class VaccinationSimpleResultDialog : Window
    {
        private readonly VaccinationRecord _vaccinationRecord;
        private readonly VaccinationRecordService _vaccinationRecordService;

        public VaccinationSimpleResultDialog(VaccinationRecord vaccinationRecord)
        {
            InitializeComponent();
            _vaccinationRecord = vaccinationRecord;
            _vaccinationRecordService = new VaccinationRecordService();
            
            LoadData();
        }

        private void LoadData()
        {
            // Display student info
            if (_vaccinationRecord.Student != null)
            {
                txtStudentInfo.Text = $"{_vaccinationRecord.Student.FullName} - Lớp: {_vaccinationRecord.Student.Class}";
            }

            // Display campaign info
            if (_vaccinationRecord.Campaign != null)
            {
                txtCampaignInfo.Text = $"{_vaccinationRecord.Campaign.VaccineName} - {_vaccinationRecord.Campaign.Date?.ToString("dd/MM/yyyy")}";
            }

            // Set current values if already vaccinated
            if (_vaccinationRecord.VaccinationDate.HasValue)
            {
                dpVaccinationDate.SelectedDate = _vaccinationRecord.VaccinationDate.Value;
            }
            else
            {
                // Default to today if not set
                dpVaccinationDate.SelectedDate = DateTime.Today;
            }

            // Set result if exists
            if (!string.IsNullOrEmpty(_vaccinationRecord.Result))
            {
                if (_vaccinationRecord.Result == "Success")
                {
                    rbSuccess.IsChecked = true;
                }
                else if (_vaccinationRecord.Result == "Failed")
                {
                    rbFailed.IsChecked = true;
                }
            }
            else
            {
                // Default to Success
                rbSuccess.IsChecked = true;
            }

            // Set notes
            txtNotes.Text = _vaccinationRecord.Notes ?? "";
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validation
                if (!dpVaccinationDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày tiêm!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (dpVaccinationDate.SelectedDate.Value > DateTime.Today)
                {
                    MessageBox.Show("Ngày tiêm không thể trong tương lai!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (!rbSuccess.IsChecked.Value && !rbFailed.IsChecked.Value)
                {
                    MessageBox.Show("Vui lòng chọn kết quả tiêm chủng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update vaccination record
                _vaccinationRecord.VaccinationDate = dpVaccinationDate.SelectedDate.Value;
                _vaccinationRecord.Result = rbSuccess.IsChecked.Value ? "Success" : "Failed";
                _vaccinationRecord.Notes = txtNotes.Text.Trim();

                var updatedRecord = _vaccinationRecordService.UpdateVaccinationRecord(_vaccinationRecord);
                
                if (updatedRecord != null)
                {
                    string resultText = rbSuccess.IsChecked.Value ? "thành công" : "thất bại";
                    MessageBox.Show($"Đã ghi nhận kết quả tiêm chủng: {resultText}!", "Thành công", 
                                  MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi ghi nhận kết quả!", "Lỗi", 
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi ghi nhận kết quả tiêm chủng: {ex.Message}", "Lỗi", 
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}