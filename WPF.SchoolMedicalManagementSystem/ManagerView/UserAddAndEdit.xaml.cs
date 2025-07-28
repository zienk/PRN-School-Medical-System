using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPF.SchoolMedicalManagementSystem.ManagerView
{
    // Form thêm mới và chỉnh sửa người dùng
    public partial class UserAddAndEdit : Window
    {
        private bool isEditMode;
        private User currentUser;
        private IRoleService _roleService;
        private IUserService _userService;

        public UserAddAndEdit(bool isEdit, User user)
        {
            InitializeComponent();

            _roleService = new RoleService();
            _userService = new UserService();

            isEditMode = isEdit;
            currentUser = user;
            LoadRoles();

            if (isEditMode)
            {
                HeaderTitle.Text = "CẬP NHẬT THÔNG TIN NGƯỜI DÙNG";
                btnSave.Content = "Cập nhật";

                // Ẩn các ô text box và label chỉ dành cho thêm mới
                UsernamePanel.Visibility = Visibility.Collapsed;
                PasswordPanel.Visibility = Visibility.Collapsed;
                ConfirmPasswordPanel.Visibility = Visibility.Collapsed;
                LoadUserData(); // Tải dữ liệu người dùng cần cập nhật
            }
            else
            {
                HeaderTitle.Text = "THÊM NGƯỜI DÙNG MỚI";
                btnSave.Content = "Thêm mới";
            }
        }

        // Tải danh sách vai trò vào ComboBox
        private void LoadRoles()
        {
            var roles = _roleService.GetAllRoles();
            cmbRole.ItemsSource = roles;
            cmbRole.DisplayMemberPath = "RoleName";
            cmbRole.SelectedValue = 1;
            cmbRole.SelectedValuePath = "RoleId";
        }

        // Tải dữ liệu người dùng vào form (chế độ chỉnh sửa)
        private void LoadUserData()
        {
            if (currentUser != null)
            {
                txtFullName.Text = currentUser.FullName;
                txtEmail.Text = currentUser.Email;
                txtPhone.Text = currentUser.Phone;
                txtAddress.Text = currentUser.Address;

                // Chọn vai trò hiện tại
                cmbRole.SelectedValue = currentUser.RoleId;
            }
        }

        // Xử lý sự kiện lưu dữ liệu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    // Kiểm tra dữ liệu đầu vào cho chế độ chỉnh sửa
                    if (string.IsNullOrWhiteSpace(txtFullName.Text))
                    {
                        MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtFullName.Focus();
                        return;
                    }
                    if (cmbRole.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        cmbRole.Focus();
                        return;
                    }
                    
                    // Cập nhật thông tin người dùng
                    currentUser.FullName = txtFullName.Text.Trim();
                    currentUser.Email = txtEmail.Text.Trim();
                    currentUser.Phone = txtPhone.Text.Trim();
                    currentUser.Address = txtAddress.Text.Trim();
                    currentUser.RoleId = (int)cmbRole.SelectedValue;

                    _userService.UpdateUser(currentUser);
                    MessageBox.Show("Cập nhật người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
                else
                {
                    // Kiểm tra dữ liệu đầu vào cho chế độ thêm mới
                    if (string.IsNullOrWhiteSpace(txtUsername.Text))
                    {
                        MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtUsername.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtPassword.Password))
                    {
                        MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtPassword.Focus();
                        return;
                    }
                    if (txtPassword.Password != txtConfirmPassword.Password)
                    {
                        MessageBox.Show("Mật khẩu xác nhận không khớp!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtConfirmPassword.Focus();
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtFullName.Text))
                    {
                        MessageBox.Show("Vui lòng nhập họ và tên!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        txtFullName.Focus();
                        return;
                    }
                    if (cmbRole.SelectedValue == null)
                    {
                        MessageBox.Show("Vui lòng chọn vai trò!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        cmbRole.Focus();
                        return;
                    }
                    
                    // Tạo người dùng mới
                    var newUser = new User
                    {
                        Username = txtUsername.Text.Trim(),
                        Password = txtPassword.Password.Trim(),
                        FullName = txtFullName.Text.Trim(),
                        Email = txtEmail.Text.Trim(),
                        Phone = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        RoleId = (int)cmbRole.SelectedValue,
                        IsActive = true,
                        IsFirstLogin = true
                    };
                    _userService.AddUser(newUser);
                    MessageBox.Show("Thêm người dùng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Xử lý sự kiện hủy
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
