using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    public class GenderToBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int genderId = value is int id ? id : 0;
            return genderId == 2
                ? new SolidColorBrush(Color.FromRgb(255, 224, 178))
                : new SolidColorBrush(Color.FromRgb(225, 245, 254));
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}