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
using WPF.SchoolMedicalManagementSystem.ViewModels;

namespace WPF.SchoolMedicalManagementSystem.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {


        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginWindowViewModels();
        }


        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginWindowViewModels viewModel)
            {
                viewModel.Password = (sender as PasswordBox)?.Password;
            }
        }

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);

            if (DataContext is LoginWindowViewModels viewModel)
            {
                viewModel.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == nameof(viewModel.Roleid))
                    {
                        if (viewModel.Roleid == 1)
                        {
                            MessageBox.Show("Đăng nhập thành công với quyền quản trị viên!");
                            MainWindow mainWindow = new MainWindow(viewModel);
                            // Truyền vai trò hoặc thông tin người dùng vào MainWindow nếu cần
                            // Ví dụ: mainWindow.CurrentUserRole = "Admin";
                            
                            mainWindow.Show();
                            this.Close();

                        }
                        else if (viewModel.Roleid == 2)
                        {
                            MainWindow mainWindow = new MainWindow(viewModel);
                            // Truyền vai trò hoặc thông tin người dùng vào MainWindow nếu cần
                            // Ví dụ: mainWindow.CurrentUserRole = "Admin";
                            mainWindow.Show();
                            this.Close();
                        }
                        else if (viewModel.Roleid == 3)
                        {
                            MessageBox.Show("Đăng nhập thành công với quyền User!");
                            MainWindow mainWindow = new MainWindow(viewModel);
                            // Truyền vai trò hoặc thông tin người dùng vào MainWindow nếu cần
                            // Ví dụ: mainWindow.CurrentUserRole = "Admin";
                            mainWindow.Show();
                            this.Close();
                        }
                    }
                };
            }
        }

    }
}