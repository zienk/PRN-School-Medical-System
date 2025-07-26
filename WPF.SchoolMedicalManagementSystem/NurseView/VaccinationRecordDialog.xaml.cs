using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects.Entities;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for VaccinationRecordDialog.xaml
    /// </summary>
    public partial class VaccinationRecordDialog : Window
    {
        private VaccinationRecordService _vaccinationRecordService;
        private VaccinationCampaignService _campaignService;
        private StudentService _studentService;
        private VaccinationRecord _vaccinationRecord;
        private bool _isEditMode = false;

        // Constructor cho thêm mới với chiến dịch được chọn trước
        public VaccinationRecordDialog(VaccinationCampaign selectedCampaign = null)
        {
            InitializeComponent();
            InitializeServices();
            LoadFormData();
            
            // Nếu có chiến dịch được chọn trước, set làm mặc định
            if (selectedCampaign != null && selectedCampaign.CampaignId > 0)
            {
                cmbCampaign.SelectedValue = selectedCampaign.CampaignId;
            }
            
            dpVaccinationDate.SelectedDate = DateTime.Now;
            txtHeader.Text = "GHI NHẬN TIÊM CHỦNG MỚI";
        }

        // Constructor cho chỉnh sửa
        public VaccinationRecordDialog(VaccinationRecord record)
        {
            InitializeComponent();
            InitializeServices();
            _vaccinationRecord = record;
            _isEditMode = true;
            LoadFormData();
            LoadRecordData();
            txtHeader.Text = "CHỈNH SỬA BẢN GHI TIÊM CHỦNG";
        }

        private void InitializeServices()
        {
            _vaccinationRecordService = new VaccinationRecordService();
            _campaignService = new VaccinationCampaignService();
            _studentService = new StudentService();
        }

        private void LoadFormData()
        {
            try
            {
                LoadCampaigns();
                LoadStudents();
                HideErrorMessage();
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi tải dữ liệu form: {ex.Message}");
            }
        }

        private void LoadCampaigns()
        {
            var campaigns = _campaignService.GetAllVaccinationCampaigns()
                .Where(c => c.IsActive == true) // Chỉ hiển thị chiến dịch đang hoạt động
                .OrderBy(c => c.VaccineName)
                .ToList();

            cmbCampaign.ItemsSource = campaigns;
            
            if (campaigns.Count > 0)
            {
                cmbCampaign.SelectedIndex = 0;
            }
        }

        private void LoadStudents()
        {
            var students = _studentService.GetAllStudents()
                .Where(s => s.IsActive == true) // Chỉ hiển thị học sinh đang hoạt động
                .OrderBy(s => s.FullName)
                .ToList();

            cmbStudent.ItemsSource = students;
        }

        private void LoadRecordData()
        {
            if (_vaccinationRecord != null)
            {
                cmbCampaign.SelectedValue = _vaccinationRecord.CampaignId;
                cmbStudent.SelectedValue = _vaccinationRecord.StudentId;
                dpVaccinationDate.SelectedDate = _vaccinationRecord.VaccinationDate;
                txtNotes.Text = _vaccinationRecord.Notes ?? string.Empty;
            }
        }

        private void CmbCampaign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCampaign.SelectedItem is VaccinationCampaign selectedCampaign)
            {
                // Hiển thị thông tin vaccine
                txtVaccineType.Text = selectedCampaign.VaccineName ?? "Không xác định";
                txtCampaignDescription.Text = selectedCampaign.Description ?? "Không có mô tả";
                pnlVaccineInfo.Visibility = Visibility.Visible;
                
                HideErrorMessage();
            }
            else
            {
                pnlVaccineInfo.Visibility = Visibility.Collapsed;
            }
        }

        private void CmbStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbStudent.SelectedItem is Student selectedStudent)
            {
                // Hiển thị thông tin học sinh
                txtStudentClass.Text = selectedStudent.Class ?? "Không xác định";
                txtStudentDOB.Text = selectedStudent.DateOfBirth?.ToString("dd/MM/yyyy") ?? "Không xác định";
                pnlStudentInfo.Visibility = Visibility.Visible;
                
                HideErrorMessage();
            }
            else
            {
                pnlStudentInfo.Visibility = Visibility.Collapsed;
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
                        UpdateRecord();
                    }
                    else
                    {
                        CreateNewRecord();
                    }
                    
                    DialogResult = true;
                    Close();
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage($"Lỗi khi lưu bản ghi: {ex.Message}");
            }
        }

        private bool ValidateForm()
        {
            // Kiểm tra chiến dịch
            if (cmbCampaign.SelectedValue == null)
            {
                ShowErrorMessage("Vui lòng chọn chiến dịch tiêm chủng!");
                cmbCampaign.Focus();
                return false;
            }

            // Kiểm tra học sinh
            if (cmbStudent.SelectedValue == null)
            {
                ShowErrorMessage("Vui lòng chọn học sinh!");
                cmbStudent.Focus();
                return false;
            }

            // Kiểm tra ngày tiêm
            if (!dpVaccinationDate.SelectedDate.HasValue)
            {
                ShowErrorMessage("Vui lòng chọn ngày tiêm!");
                dpVaccinationDate.Focus();
                return false;
            }

            // Kiểm tra ngày tiêm không được trong tương lai
            if (dpVaccinationDate.SelectedDate.Value.Date > DateTime.Now.Date)
            {
                ShowErrorMessage("Ngày tiêm không thể trong tương lai!");
                dpVaccinationDate.Focus();
                return false;
            }

            // Kiểm tra trùng lặp (chỉ khi thêm mới hoặc thay đổi học sinh/chiến dịch)
            if (!_isEditMode || 
                (int)cmbStudent.SelectedValue != _vaccinationRecord.StudentId ||
                (int)cmbCampaign.SelectedValue != _vaccinationRecord.CampaignId)
            {
                if (CheckDuplicateRecord())
                {
                    ShowErrorMessage("Học sinh này đã có bản ghi tiêm chủng cho chiến dịch này!");
                    return false;
                }
            }

            return true;
        }

        private bool CheckDuplicateRecord()
        {
            try
            {
                int studentId = (int)cmbStudent.SelectedValue;
                int campaignId = (int)cmbCampaign.SelectedValue;
                
                var existingRecords = _vaccinationRecordService.GetVaccinationRecordsByCampaignId(campaignId);
                return existingRecords.Any(r => r.StudentId == studentId && r.IsActive == true);
            }
            catch
            {
                return false; // Nếu có lỗi, cho phép tiếp tục và để service layer xử lý
            }
        }

        private void CreateNewRecord()
        {
            var newRecord = new VaccinationRecord
            {
                StudentId = (int)cmbStudent.SelectedValue,
                CampaignId = (int)cmbCampaign.SelectedValue,
                VaccinationDate = dpVaccinationDate.SelectedDate.Value,
                Notes = txtNotes.Text?.Trim(),
                IsActive = true
            };

            _vaccinationRecordService.AddVaccinationRecord(newRecord);
            MessageBox.Show("Đã ghi nhận tiêm chủng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void UpdateRecord()
        {
            if (_vaccinationRecord != null)
            {
                _vaccinationRecord.StudentId = (int)cmbStudent.SelectedValue;
                _vaccinationRecord.CampaignId = (int)cmbCampaign.SelectedValue;
                _vaccinationRecord.VaccinationDate = dpVaccinationDate.SelectedDate.Value;
                _vaccinationRecord.Notes = txtNotes.Text?.Trim();

                _vaccinationRecordService.UpdateVaccinationRecord(_vaccinationRecord);
                MessageBox.Show("Đã cập nhật bản ghi tiêm chủng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowErrorMessage(string message)
        {
            txtErrorMessage.Text = message;
            txtErrorMessage.Visibility = Visibility.Visible;
        }

        private void HideErrorMessage()
        {
            txtErrorMessage.Visibility = Visibility.Collapsed;
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}