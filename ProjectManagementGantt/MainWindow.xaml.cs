using ProjectManagement.Models;
using ProjectManagement.DAL;
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

namespace ProjectManagement
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ProjectManagementContext db = new ProjectManagementContext();
        public MainWindow()
        {
            InitializeComponent();

            //Database.SetInitializer<ObjectContext>(new MigrateDatabaseToLatestVersion<ObjectContext, Configuration>());

            Employee employee = new Employee { FirstName = "Carson", LastName = "Alexander", Department = "Buero", Phone = "0000" };
            //Employees.Add(employee);
            db.Employees.Add(employee);
            db.SaveChanges();

            //Employee employee2 = db.Employees.Find(1);
            //employee2.Department = "New Buero";

            var blog = db.Employees.Single(b => b.Id == 1);
            MessageBox.Show("Name: " + blog.FirstName + " " + blog.LastName + "  -  " + blog.Department);
            blog.Department = "New Buero";
            MessageBox.Show("Name: " + blog.FirstName + " " + blog.LastName + "  -  " + blog.Department);
            db.SaveChanges();

        }

    }
}
