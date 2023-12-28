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
    /// Interaktionslogik für Main.xaml
    /// </summary>
    public partial class Main : Window
    {
        private MainViewModel model;
        
        public Main()
        {
            InitializeComponent();
            model = new MainViewModel();
            this.DataContext = model;
        }
    }

}
