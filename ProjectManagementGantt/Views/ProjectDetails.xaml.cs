using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaktionslogik für ProjectDetails.xaml
    /// </summary>
    public partial class ProjectDetails : Window
    {
        private ProjectDetailsViewModel model;
        public bool IsClosed { get; private set; }

        public ProjectDetails(ProjectManagementContext Context)
        {
            InitializeComponent();
            model = new ProjectDetailsViewModel(Context);
            this.DataContext = model;
        }

        public ProjectDetails(ProjectManagementContext Context, Project Project)
        {
            InitializeComponent();
            model = new ProjectDetailsViewModel(Context, Project);
            this.DataContext = model;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
