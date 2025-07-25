using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Windows;
using System.Linq;

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    public partial class StudentAddAndEdit : Window
    {
        private bool isEditMode;
        private Student currentStudent;
        private IStudentService _studentService;
        private IGenderTypeService _genderService;
        private IUserService _userService;

        const int PARENT_ROLE_ID = 3;

        public StudentAddAndEdit(bool isEdit, Student student)
        {
            InitializeComponent();

            _studentService = new StudentService();
            _genderService = new GenderTypeService();
            _userService = new UserService();

            isEditMode = isEdit;
            currentStudent = student;

            LoadGenders();
            LoadParents();

            if (isEditMode)
            {
                HeaderTitle.Text = "CẬP NHẬT THÔNG TIN HỌC SINH";
                btnSave.Content = "Cập nhật";
                LoadStudentData();
            }
            else
            {
                HeaderTitle.Text = "THÊM HỌC SINH MỚI";
                btnSave.Content = "Thêm mới";
                chkIsActive.IsChecked = true;
            }
        }

        private void LoadGenders()
        {
            var genders = _genderService.GetAllGenderTypes();
            cmbGender.ItemsSource = genders;
            cmbGender.DisplayMemberPath = "GenderName";
            cmbGender.SelectedValuePath = "GenderId";
        }

        private void LoadParents()
        {
            var parents = _userService.GetUsersByRole(PARENT_ROLE_ID);
            cmbParent.ItemsSource = parents;
            cmbParent.DisplayMemberPath = "FullName";
            cmbParent.SelectedValuePath = "UserId";
        }

        private void LoadStudentData()
        {
            if (currentStudent != null)
            {
                txtFullName.Text = currentStudent.FullName;
                dpDateOfBirth.SelectedDate = currentStudent.DateOfBirth?.ToDateTime(TimeOnly.MinValue);
                cmbGender.SelectedValue = currentStudent.GenderId;
                txtClass.Text = currentStudent.Class;
                cmbParent.SelectedValue = currentStudent.ParentId;
                chkIsActive.IsChecked = currentStudent.IsActive;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInputs()) return;

                if (isEditMode)
                {
                    currentStudent.FullName = txtFullName.Text.Trim();
                    currentStudent.DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null;
                    currentStudent.GenderId = (int)cmbGender.SelectedValue;
                    currentStudent.Class = txtClass.Text.Trim();
                    currentStudent.ParentId = (Guid)cmbParent.SelectedValue;
                    currentStudent.IsActive = chkIsActive.IsChecked.Value;

                    _studentService.UpdateStudent(currentStudent);
                    MessageBox.Show(
                        "Cập nhật học sinh thành công!", 
                        "Thông báo", 
                        MessageBoxButton.OK, 
                        MessageBoxImage.Information);
                }
                else
                {
                    var newStudent = new Student
                    {
                        FullName = txtFullName.Text.Trim(),
                        DateOfBirth = dpDateOfBirth.SelectedDate.HasValue ? DateOnly.FromDateTime(dpDateOfBirth.SelectedDate.Value) : null,
                        GenderId = (int)cmbGender.SelectedValue,
                        Class = txtClass.Text.Trim(),
                        ParentId = (Guid)cmbParent.SelectedValue,
                        IsActive = chkIsActive.IsChecked.Value
                    };
                    _studentService.AddStudent(newStudent);
                    MessageBox.Show("Thêm học sinh thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtFullName.Focus();
                return false;
            }
            if (dpDateOfBirth.SelectedDate == null)
            {
                MessageBox.Show("Vui lòng chọn ngày sinh!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpDateOfBirth.Focus();
                return false;
            }
            if (cmbGender.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn giới tính!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbGender.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtClass.Text))
            {
                MessageBox.Show("Vui lòng nhập lớp học!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtClass.Focus();
                return false;
            }
            if (cmbParent.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn phụ huynh!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                cmbParent.Focus();
                return false;
            }
            return true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        

    }
} 