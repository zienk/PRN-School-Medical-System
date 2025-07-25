using System;
using System.Collections.Generic;
using System.Windows;
using BusinessObjects.Entities;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    public partial class MedicalEventAddAndEdit : Window
    {
        private readonly IncidentService _incidentService;
        private readonly StudentService _studentService;
        private readonly UserService _userService;
        private readonly RoleService _roleService;
        private readonly SeverityLevelService _severityService;
        private readonly MedicalEventTypeService _eventTypeService;
        private readonly bool _isEditMode;
        private Incident _incident;
        private List<Student> _students;
        private List<User> _nurses;
        private List<SeverityLevel> _severities;
        private List<MedicalEventType> _eventTypes;

        public MedicalEventAddAndEdit(Incident incident = null)
        {
            InitializeComponent();
            _incidentService = new IncidentService();
            _studentService = new StudentService();
            _userService = new UserService();
            _roleService = new RoleService();
            _severityService = new SeverityLevelService();
            _eventTypeService = new MedicalEventTypeService();
            _isEditMode = incident != null;
            _incident = incident ?? new Incident();
            Loaded += MedicalEventAddAndEdit_Loaded;
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, e) => Close();
        }

        private void MedicalEventAddAndEdit_Loaded(object sender, RoutedEventArgs e)
        {
            _students = _studentService.GetAllStudents();
            cbStudent.ItemsSource = _students;
            _eventTypes = _eventTypeService.GetAllMedicalEventTypes();
            cbEventType.ItemsSource = _eventTypes;
            _severities = _severityService.GetAllSeverityLevels();
            cbSeverity.ItemsSource = _severities;
            // Lọc user có role là nurse
            var nurseRole = _roleService.GetAllRoles().Find(r => r.RoleName != null && r.RoleName.ToLower().Contains("nurse"));
            _nurses = nurseRole != null ? _userService.GetAllUsers().FindAll(u => u.RoleId == nurseRole.RoleId) : new List<User>();
            cbReportedBy.ItemsSource = _nurses;

            if (_isEditMode)
            {
                cbStudent.SelectedItem = _students.Find(s => s.StudentId == _incident.StudentId);
                cbEventType.SelectedItem = _eventTypes.Find(e => e.EventTypeId == _incident.IncidentTypeId);
                dpIncidentDate.SelectedDate = _incident.IncidentDate;
                txtDescription.Text = _incident.Description;
                cbSeverity.SelectedItem = _severities.Find(s => s.SeverityId == _incident.SeverityId);
                cbReportedBy.SelectedItem = _nurses.Find(u => u.UserId == _incident.ReportedBy);
                txtLocation.Text = _incident.Location;
                txtActionsTaken.Text = _incident.ActionsTaken;
            }
            else
            {
                cbStudent.SelectedIndex = -1;
                cbEventType.SelectedIndex = -1;
                dpIncidentDate.SelectedDate = DateTime.Now;
                txtDescription.Text = string.Empty;
                cbSeverity.SelectedIndex = -1;
                cbReportedBy.SelectedIndex = -1;
                txtLocation.Text = string.Empty;
                txtActionsTaken.Text = string.Empty;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (cbStudent.SelectedItem is not Student selectedStudent)
            {
                MessageBox.Show("Vui lòng chọn học sinh.");
                return;
            }
            if (cbEventType.SelectedItem is not MedicalEventType selectedEventType)
            {
                MessageBox.Show("Vui lòng chọn loại sự kiện.");
                return;
            }
            if (dpIncidentDate.SelectedDate is not DateTime selectedDate)
            {
                MessageBox.Show("Vui lòng chọn ngày sự kiện.");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDescription.Text))
            {
                MessageBox.Show("Vui lòng nhập mô tả sự kiện.");
                return;
            }
            if (cbSeverity.SelectedItem is not SeverityLevel selectedSeverity)
            {
                MessageBox.Show("Vui lòng chọn mức độ.");
                return;
            }
            if (cbReportedBy.SelectedItem is not User selectedNurse)
            {
                MessageBox.Show("Vui lòng chọn người báo cáo.");
                return;
            }
            _incident.StudentId = selectedStudent.StudentId;
            _incident.IncidentTypeId = selectedEventType.EventTypeId;
            _incident.IncidentDate = selectedDate;
            _incident.Description = txtDescription.Text.Trim();
            _incident.SeverityId = selectedSeverity.SeverityId;
            _incident.ReportedBy = selectedNurse.UserId;
            _incident.Location = txtLocation.Text.Trim();
            _incident.ActionsTaken = txtActionsTaken.Text.Trim();
            _incident.IsActive = true;
            try
            {
                if (_isEditMode)
                {
                    _incidentService.UpdateIncident(_incident);
                    MessageBox.Show("Cập nhật sự kiện y tế thành công!");
                }
                else
                {
                    _incidentService.AddIncident(_incident);
                    MessageBox.Show("Thêm sự kiện y tế thành công!");
                }
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
        }
    }
} 