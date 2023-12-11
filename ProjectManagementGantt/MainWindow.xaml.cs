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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManagementGantt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow _instance;
        public static EmployeesWindow employeesWindow;

        public MainWindow()
        {
            _instance = this;
            InitializeComponent();
        }
        

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            employeesWindow = new EmployeesWindow();
            employeesWindow.Show();
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            ProjectsWindow projectsWindow = new ProjectsWindow();
            projectsWindow.Show();
        }
    }
}
