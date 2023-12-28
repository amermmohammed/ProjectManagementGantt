using ProjectManagement.DAL;
using ProjectManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Interaktionslogik für EmployeeList.xaml
    /// </summary>
    public partial class EmployeeList : Window
    {
        private EmployeeListViewModel model;
        public bool IsClosed { get; private set; }

        public EmployeeList(ProjectManagementContext Context)
        {
            InitializeComponent();
            model = new EmployeeListViewModel(Context);
            this.DataContext = model;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

    }
}
