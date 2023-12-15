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
using System.Data.SQLite;

namespace ProjectManagementGantt
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public static MainWindow _instance;
        public static EmployeeOverview employeeOverview;
        public static ProjectOverview projectOverview;

        public MainWindow()
        {
            _instance = this;
            InitializeComponent();
        }

        private void btnEmployees_Click(object sender, RoutedEventArgs e)
        {
            employeeOverview = new EmployeeOverview();
            employeeOverview.Show();
        }

        private void btnProjects_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                projectOverview = new ProjectOverview();
                projectOverview.Show();
            } catch(Exception err)
            {
                MessageBox.Show("Error: " + err);
            }
        }
    }
}
