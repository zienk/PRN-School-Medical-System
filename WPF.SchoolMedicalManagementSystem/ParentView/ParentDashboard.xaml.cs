﻿using BusinessObjects.Entities;
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
using WPF.SchoolMedicalManagementSystem.ParentView;


namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    /// <summary>
    /// Interaction logic for ParentDashboard.xaml
    /// </summary>
    public partial class ParentDashboard : Window
    {
        private User user;
        public ParentDashboard(User currentUser)
        {
            InitializeComponent();
            user = currentUser;
        }

        private void btnHealthRecord_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new Services.Implementations.StudentService();
            var students = studentService.GetAllStudentsByUserId(user.UserId);

            if (students == null || students.Count == 0)
            {
                MessageBox.Show("Phụ huynh này chưa có học sinh nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var studentInfoWindow = new StudentInfo(students, user);
            studentInfoWindow.Show();
            this.Close();
        }

        private void btnIncidents_Click(object sender, RoutedEventArgs e)
        {
            MedicalIncidentsManagement medicalIncidentsManagement = new MedicalIncidentsManagement(user);
            medicalIncidentsManagement.Show();
            this.Close();
        }

        private void btnHealthCheckups_Click(object sender, RoutedEventArgs e)
        {
            HealthCheckupResultManagement healthCheckupManagement = new HealthCheckupResultManagement(user);
            healthCheckupManagement.Show();
            this.Close();
        }

        private void btnVaccinations_Click(object sender, RoutedEventArgs e)
        {
            VaccineResultManagement vaccineResultManagement = new VaccineResultManagement(user);
            vaccineResultManagement.Show();
            this.Close();
        }

        private void btnStudentInfo_Click(object sender, RoutedEventArgs e)
        {
            var studentService = new Services.Implementations.StudentService();
            var students = studentService.GetAllStudentsByUserId(user.UserId);

            if (students == null || students.Count == 0)
            {
                MessageBox.Show("Phụ huynh này chưa có học sinh nào!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var studentInfoWindow = new StudentInfo(students, user);
            studentInfoWindow.Show();
            this.Close();
        }
    }
}
