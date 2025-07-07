using System.Windows;

namespace WPF.SchoolMedicalManagementSystem.ParentView
{
    public partial class IncidentDetailsWindow : Window
    {
        public IncidentDetailsWindow(object incident)
        {
            InitializeComponent();
            DataContext = incident;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}