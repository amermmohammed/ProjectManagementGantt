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
    /// Interaktionslogik für GanttChart.xaml
    /// </summary>
    public partial class GanttChart : Window
    {
        private readonly GanttChartViewModel model;
        public bool IsClosed { get; private set; }

        public GanttChart(ProjectManagementContext Context, Project Project)
        {
            InitializeComponent();
            model = new GanttChartViewModel(Context, Project);
            this.DataContext = model;


            

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            IsClosed = true;
        }

    }
}
