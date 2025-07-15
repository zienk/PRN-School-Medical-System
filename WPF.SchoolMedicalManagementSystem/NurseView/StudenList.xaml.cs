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
using Services.Implementations;
using Services.Interfaces;

namespace WPF.SchoolMedicalManagementSystem
{
    /// <summary>
    /// Interaction logic for StudentRecordManagement.xaml
    /// </summary>
    public partial class StudentRecordManagement : Window
    {
        private IHealthRecordService _healthRecordService;

        public StudentRecordManagement()
        {
            InitializeComponent();

            LoadData();
        }

        public void LoadData()
        {
            _healthRecordService = new HealthRecordService();
            dgListStudent.ItemsSource = null;
            dgListStudent.ItemsSource = _healthRecordService.GetAllHealthRecord();
        }

        private void SearchTextBox_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
