using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using BusinessObjects.Entities;
using Services.Implementations;

namespace WPF.SchoolMedicalManagementSystem.NurseView
{
    public partial class VaccinationStudentListDialog : Window
    {
        private readonly int _campaignId;
        // ✅ FIXED: Changed from List<> to ObservableCollection for proper WPF binding
        private ObservableCollection<StudentSelectable> _students;
        // ✅ FIXED: Added CollectionViewSource for filtering without breaking binding
        private CollectionViewSource _studentsViewSource;
        private List<string> _classList;
        private List<string> _genderList;

        public List<int> SelectedStudentIds { get; private set; } = new List<int>();

        public VaccinationStudentListDialog(int campaignId)
        {
            InitializeComponent();
            _campaignId = campaignId;
            
            // ✅ FIXED: Initialize ObservableCollection and CollectionViewSource for stable binding
            _students = new ObservableCollection<StudentSelectable>();
            _studentsViewSource = new CollectionViewSource { Source = _students };
            // ✅ FIXED: Set ItemsSource only ONCE to maintain binding stability
            dgStudents.ItemsSource = _studentsViewSource.View;
            
            LoadFilters();
            LoadStudents();
        }

        private void LoadFilters()
        {
            var studentService = new StudentService();
            var allStudents = studentService.GetAllStudents().Where(s => s.IsActive == true).ToList();
            _classList = allStudents.Select(s => s.Class).Distinct().OrderBy(c => c).ToList();
            _genderList = allStudents.Select(s => s.GenderId == 1 ? "Nam" : "Nữ").Distinct().ToList();
            cmbClassFilter.ItemsSource = _classList;
            cmbGenderFilter.ItemsSource = _genderList;
        }

        private void LoadStudents()
        {
            var studentService = new StudentService();
            var vaccinationRecordService = new VaccinationRecordService();
            
            var allStudents = studentService.GetAllStudents().Where(s => s.IsActive == true).ToList();
            
            // ✅ ADDED: Kiểm tra học sinh nào đã có trong đợt tiêm này
            var existingRecords = vaccinationRecordService.GetVaccinationRecordsByCampaignId(_campaignId);
            var existingStudentIds = existingRecords.Where(r => r.IsActive == true)
                                                   .Select(r => r.StudentId)
                                                   .ToHashSet();
            
            // ✅ FIXED: Use ObservableCollection.Clear() and Add() to maintain binding
            _students.Clear();
            foreach (var student in allStudents)
            {
                _students.Add(new StudentSelectable(student, existingStudentIds.Contains(student.StudentId)));
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            // ✅ FIXED: Use CollectionView.Filter instead of changing ItemsSource
            // This preserves checkbox binding and selected state
            _studentsViewSource.View.Filter = (obj) =>
            {
                if (obj is StudentSelectable student)
                {
                    bool matchClass = cmbClassFilter.SelectedItem == null || student.Class == (string)cmbClassFilter.SelectedItem;
                    bool matchName = string.IsNullOrWhiteSpace(txtNameFilter.Text) || 
                                   student.FullName.IndexOf(txtNameFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0;
                    bool matchGender = cmbGenderFilter.SelectedItem == null || student.GenderName == (string)cmbGenderFilter.SelectedItem;
                    
                    return matchClass && matchName && matchGender;
                }
                return false;
            };
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            cmbClassFilter.SelectedItem = null;
            txtNameFilter.Text = string.Empty;
            cmbGenderFilter.SelectedItem = null;
            // ✅ FIXED: Clear filter without changing ItemsSource
            _studentsViewSource.View.Filter = null;
        }

        private void ChkSelectAll_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = (chkSelectAll.IsChecked == true);
            
            // ✅ FIXED: Select/deselect only visible students (after filter)
            // ✅ ADDED: Chỉ select những học sinh chưa được thêm vào đợt tiêm
            foreach (StudentSelectable student in _studentsViewSource.View)
            {
                if (!student.IsAlreadyAdded) // Chỉ select học sinh chưa thêm
                {
                    student.IsSelected = isChecked; // INotifyPropertyChanged will update UI
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // ✅ FIXED: Get all selected students from original collection (not filtered view)
            // ✅ ADDED: Chỉ lấy những học sinh chưa được thêm vào đợt tiêm
            SelectedStudentIds = _students.Where(s => s.IsSelected && !s.IsAlreadyAdded)
                                         .Select(s => s.StudentId).ToList();
            
            if (SelectedStudentIds.Count == 0)
            {
                var alreadyAddedCount = _students.Count(s => s.IsSelected && s.IsAlreadyAdded);
                if (alreadyAddedCount > 0)
                {
                    MessageBox.Show($"Các học sinh đã chọn đều đã có trong đợt tiêm này rồi!\nVui lòng chọn học sinh khác.", 
                                  "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn ít nhất một học sinh!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                return;
            }
            
            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }

    // ✅ FIXED: StudentSelectable class with proper INotifyPropertyChanged implementation
    public class StudentSelectable : Student, INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set { 
                // ✅ ADDED: Không cho phép select học sinh đã thêm rồi
                if (!IsAlreadyAdded)
                {
                    _isSelected = value; 
                    OnPropertyChanged("IsSelected"); // Notify UI when checkbox changes
                }
            }
        }
        
        // ✅ ADDED: Property để biết học sinh đã được thêm vào đợt tiêm chưa
        public bool IsAlreadyAdded { get; private set; }
        
        // ✅ ADDED: Hiển thị trạng thái cho user
        public string StatusText => IsAlreadyAdded ? "Đã thêm" : "Chưa thêm";
        
        public string GenderName => GenderId == 1 ? "Nam" : "Nữ";
        
        // ✅ UPDATED: Constructor với parameter kiểm tra trạng thái
        public StudentSelectable(Student s, bool isAlreadyAdded = false)
        {
            this.StudentId = s.StudentId;
            this.FullName = s.FullName;
            this.Class = s.Class;
            this.DateOfBirth = s.DateOfBirth;
            this.GenderId = s.GenderId;
            this.IsActive = s.IsActive;
            this.IsAlreadyAdded = isAlreadyAdded;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    // ✅ ADDED: Converter để disable checkbox cho học sinh đã thêm
    public class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolValue)
                return !boolValue;
            return false;
        }
    }
} 