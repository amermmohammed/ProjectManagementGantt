using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.ViewModels;
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

namespace ProjectManagement.Views
{
    /// <summary>
    /// Interaktionslogik für EmployeeDetails.xaml
    /// </summary>
    public partial class EmployeeDetails : Window
    {
        private EmployeeDetailsViewModel model;
        public bool IsClosed { get; private set; }

        public EmployeeDetails(ProjectManagementContext Context)
        {
            InitializeComponent();
            model = new EmployeeDetailsViewModel(Context);
            this.DataContext = model;
            model.OnRequestClose += (s, e) => this.Close();
        }

        public EmployeeDetails(ProjectManagementContext Context, Employee Employee)
        {
            InitializeComponent();
            model = new EmployeeDetailsViewModel(Context, Employee);
            this.DataContext = model;
            model.OnRequestClose += (s, e) => this.Close();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

    }
}
