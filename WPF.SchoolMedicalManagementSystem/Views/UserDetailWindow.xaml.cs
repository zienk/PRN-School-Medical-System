using BusinessObjects.Entities;
using Services.Implementations;
using Services.Interfaces;
using System.Linq;
using System.Windows;

namespace WPF.SchoolMedicalManagementSystem.Views
{
    public partial class UserDetailWindow : Window
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private User _user;
        private bool _isEditMode;

        public UserDetailWindow(IUserService userService, User user = null)
        {
            InitializeComponent();
            _userService = userService;
            _roleService = new RoleService();

            if (user == null)
            {
                _user = new User();
                _isEditMode = false;
                Title = "Thêm người dùng mới";
            }
            else
            {
                _user = user;
                _isEditMode = true;
                Title = "Sửa thông tin người dùng";
                txtUsername.IsEnabled = false;
                LoadUserData();
            }

            LoadRoles();
        }

        private void LoadUserData()
        {
            txtUsername.Text = _user.Username;
            txtFullName.Text = _user.FullName;
            txtEmail.Text = _user.Email;
            txtPhone.Text = _user.Phone;
            txtAddress.Text = _user.Address;
        }

        private async void LoadRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRolesAsync();
                cmbRole.ItemsSource = roles;
                cmbRole.DisplayMemberPath = "RoleName";
                cmbRole.SelectedValuePath = "RoleId";

                if (_isEditMode)
                {
                    cmbRole.SelectedValue = _user.RoleId;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách vai trò: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _user.Username = txtUsername.Text;
            _user.FullName = txtFullName.Text;
            _user.Email = txtEmail.Text;
            _user.Phone = txtPhone.Text;
            _user.Address = txtAddress.Text;
            _user.RoleId = (int)cmbRole.SelectedValue;

            try
            {
                if (_isEditMode)
                {
                    await _userService.UpdateUserAsync(_user);
                }
                else
                {
                    await _userService.AddUserAsync(_user);
                }
                DialogResult = true;
                Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Lỗi khi lưu người dùng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}