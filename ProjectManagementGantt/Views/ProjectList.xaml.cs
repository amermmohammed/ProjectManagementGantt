using ProjectManagement.DAL;
using ProjectManagement.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für ProjectList.xaml
    /// </summary>
    public partial class ProjectList : Window
    {
        private ProjectListViewModel model;
        public bool IsClosed { get; private set; }

        public ProjectList(ProjectManagementContext Context)
        {
            InitializeComponent();
            model = new ProjectListViewModel(Context);
            this.DataContext = model;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

    }
}
