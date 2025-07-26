using System;
using System.Windows;
using BusinessObjects.Entities;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    /// <summary>
    /// Interaction logic for VaccinationRecordDetailDialog.xaml
    /// </summary>
    public partial class VaccinationRecordDetailDialog : Window
    {
        private VaccinationRecord _vaccinationRecord;
        private VaccinationRecordManagement? _parentWindow;

        public VaccinationRecordDetailDialog(VaccinationRecord record, VaccinationRecordManagement? parentWindow = null)
        {
            InitializeComponent();
            _vaccinationRecord = record;
            _parentWindow = parentWindow;
            LoadRecordDetails();
        }

        private void LoadRecordDetails()
        {
            try
            {
                if (_vaccinationRecord == null)
                {
                    MessageBox.Show("Không có dữ liệu bản ghi để hiển thị.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    Close();
                    return;
                }

                // Load services to get full data if needed
                var studentService = new StudentService();
                var campaignService = new VaccinationCampaignService();

                // Thông tin bản ghi
                txtRecordId.Text = _vaccinationRecord.VaccinationRecordId.ToString();
                txtVaccinationDate.Text = _vaccinationRecord.VaccinationDate?.ToString("dd/MM/yyyy HH:mm") ?? "Không xác định";
                txtStatus.Text = _vaccinationRecord.IsActive == true ? "✅ Hoạt động" : "❌ Không hoạt động";
                txtCreatedDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm"); // Placeholder - thay bằng CreatedDate nếu có

                // Thông tin học sinh
                if (_vaccinationRecord.Student != null)
                {
                    txtStudentName.Text = _vaccinationRecord.Student.FullName ?? "Không xác định";
                    txtStudentClass.Text = _vaccinationRecord.Student.Class ?? "Không xác định";
                    txtStudentDOB.Text = _vaccinationRecord.Student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "Không xác định";
                    
                    // Get gender info
                    var genderService = new GenderTypeService();
                    var genders = genderService.GetAllGenderTypes();
                    var gender = genders.Find(g => g.GenderId == _vaccinationRecord.Student.GenderId);
                    txtStudentGender.Text = gender?.GenderName ?? "Không xác định";
                }
                else if (_vaccinationRecord.StudentId.HasValue)
                {
                    // Load student data if not included
                    var student = studentService.GetAllStudents().Find(s => s.StudentId == _vaccinationRecord.StudentId.Value);
                    if (student != null)
                    {
                        txtStudentName.Text = student.FullName ?? "Không xác định";
                        txtStudentClass.Text = student.Class ?? "Không xác định";
                        txtStudentDOB.Text = student.DateOfBirth?.ToString("dd/MM/yyyy") ?? "Không xác định";
                        
                        var genderService = new GenderTypeService();
                        var genders = genderService.GetAllGenderTypes();
                        var gender = genders.Find(g => g.GenderId == student.GenderId);
                        txtStudentGender.Text = gender?.GenderName ?? "Không xác định";
                    }
                    else
                    {
                        txtStudentName.Text = "Không tìm thấy thông tin học sinh";
                        txtStudentClass.Text = "N/A";
                        txtStudentDOB.Text = "N/A";
                        txtStudentGender.Text = "N/A";
                    }
                }
                else
                {
                    txtStudentName.Text = "Không có thông tin học sinh";
                    txtStudentClass.Text = "N/A";
                    txtStudentDOB.Text = "N/A";
                    txtStudentGender.Text = "N/A";
                }

                // Thông tin chiến dịch
                if (_vaccinationRecord.Campaign != null)
                {
                    txtCampaignName.Text = _vaccinationRecord.Campaign.VaccineName ?? "Không xác định";
                    txtCampaignDescription.Text = _vaccinationRecord.Campaign.Description ?? "Không có mô tả";
                    txtCampaignStartDate.Text = _vaccinationRecord.Campaign.Date?.ToString("dd/MM/yyyy") ?? "Không xác định";
                    txtCampaignEndDate.Text = _vaccinationRecord.Campaign.IsActive == true ? "✅ Hoạt động" : "❌ Không hoạt động";
                }
                else if (_vaccinationRecord.CampaignId.HasValue)
                {
                    // Load campaign data if not included
                    var campaign = campaignService.GetVaccinationCampaignById(_vaccinationRecord.CampaignId.Value);
                    if (campaign != null)
                    {
                        txtCampaignName.Text = campaign.VaccineName ?? "Không xác định";
                        txtCampaignDescription.Text = campaign.Description ?? "Không có mô tả";
                        txtCampaignStartDate.Text = campaign.Date?.ToString("dd/MM/yyyy") ?? "Không xác định";
                        txtCampaignEndDate.Text = campaign.IsActive == true ? "✅ Hoạt động" : "❌ Không hoạt động";
                    }
                    else
                    {
                        txtCampaignName.Text = "Không tìm thấy thông tin chiến dịch";
                        txtCampaignDescription.Text = "N/A";
                        txtCampaignStartDate.Text = "N/A";
                        txtCampaignEndDate.Text = "N/A";
                    }
                }
                else
                {
                    txtCampaignName.Text = "Không có thông tin chiến dịch";
                    txtCampaignDescription.Text = "N/A";
                    txtCampaignStartDate.Text = "N/A";
                    txtCampaignEndDate.Text = "N/A";
                }

                // Ghi chú
                txtNotes.Text = string.IsNullOrWhiteSpace(_vaccinationRecord.Notes) ? "Không có ghi chú" : _vaccinationRecord.Notes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin chi tiết: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Mở dialog chỉnh sửa
                var editDialog = new VaccinationRecordDialog(_vaccinationRecord);
                if (editDialog.ShowDialog() == true)
                {
                    // Refresh parent window if available
                    _parentWindow?.LoadData();
                    
                    // Reload current dialog with updated data
                    var recordService = new VaccinationRecordService();
                    var updatedRecord = recordService.GetVaccinationRecordById(_vaccinationRecord.VaccinationRecordId);
                    if (updatedRecord != null)
                    {
                        _vaccinationRecord = updatedRecord;
                        LoadRecordDetails();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở dialog chỉnh sửa: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}