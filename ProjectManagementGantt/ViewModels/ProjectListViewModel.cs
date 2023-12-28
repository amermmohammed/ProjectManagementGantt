using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.ViewModels
{
    public class ProjectListViewModel: INotifyPropertyChanged
    {
        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private ProjectManagementContext Context;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ObservableCollection<Project> Projects { get; set; }

        /*
        private BindingList<Project> _Projects { get; set; }
        public BindingList<Project> Projects
        {
            get => _Projects;
            set
            {
                if (value == _Projects) return;
                _Projects = value;
                OnPropertyChanged();
            }
        }*/

        private ProjectDetails projectDetailsWindow;
        private GanttChart ganttChartWindow;

        public RelayCommand ShowProjectDetailsWindowCommand { get; set; }
        public RelayCommand DeleteSelectedProjectCommand { get; set; }
        public RelayCommand ShowGanttChartWindowCommand { get; set; }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public ProjectListViewModel(ProjectManagementContext Context)
        {
            ShowProjectDetailsWindowCommand = new RelayCommand(ShowProjectDetailsWindow, CanShowProjectDetailsWindow);
            DeleteSelectedProjectCommand = new RelayCommand(DeleteSelectedProject, CanDeleteSelectedProject);
            ShowGanttChartWindowCommand = new RelayCommand(ShowGanttChartWindow, CanShowGanttChartWindow);

            this.Context = Context;
            //Context.Projects.Load();
            Projects = Context.Projects.Local.ToObservableCollection();
            //Projects = Context.Projects.Local.ToBindingList();
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool CanShowProjectDetailsWindow(object obj)
        {
            return true;
        }

        private void ShowProjectDetailsWindow(object obj)
        {
            if (isProjectDetailsWindowOpen() || isGanttChartWindowOpen())
            {
                MessageBox.Show("Bitte schließen Sie die offenen Projekt/Gantt Fenster um ein neues zu öffnen.");
                return;
            }

            //MessageBox.Show("Window");

            if (obj is String name && name == "New")
            {
                //MessageBox.Show("New Window");
                projectDetailsWindow = new ProjectDetails(Context);
                projectDetailsWindow.Show();
            }
            else
            {
                if (obj is Project project)
                {
                    projectDetailsWindow = new ProjectDetails(Context, project);
                    projectDetailsWindow.Show();
                }
                else
                {
                    MessageBox.Show("No row selected");
                }
            }
        }

        private bool CanDeleteSelectedProject(object obj)
        {
            return true;
        }

        private void DeleteSelectedProject(object obj)
        {
            if (isProjectDetailsWindowOpen() || isGanttChartWindowOpen())
            {
                MessageBox.Show("Bitte schließen Sie die offenen Projekt/Gantt Fenster um ein Projekt zu löschen.");
                return;
            }

            if (obj is Project project)
            {
                MessageBoxResult resultMessageBox = MessageBox.Show(
                    "Möchten Sie das ausgewählte Projekt wirklich löschen?\n\nId: " + project.Id + " \nTitel: " + project.Title,
                    "Info",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.None);

                if (resultMessageBox == MessageBoxResult.Yes)
                {
                    Context.Projects.Remove(project);
                    Context.SaveChanges();
                }

            }
            else
            {
                MessageBox.Show("No row selected");
            }

        }

        private bool CanShowGanttChartWindow(object obj)
        {
            return true;
        }

        private void ShowGanttChartWindow(object obj)
        {
            if (isProjectDetailsWindowOpen() || isGanttChartWindowOpen())
            {
                MessageBox.Show("Bitte schließen Sie die offenen Projekt/Gantt Fenster um ein neues zu öffnen.");
                return;
            }

            if (obj is Project project)
            {
                if (project.Phases == null)
                {
                    MessageBox.Show("Das Projekt hat keine Phasen.");
                    return;
                }
                ganttChartWindow = new GanttChart(Context, project);
                ganttChartWindow.Show();
            }
            else
            {
                MessageBox.Show("No row selected");
            }
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool isProjectDetailsWindowOpen()
        {
            if (projectDetailsWindow == null || projectDetailsWindow.IsClosed)
            {
                return false;
            }
            return true;
        }

        private bool isGanttChartWindowOpen()
        {
            if (ganttChartWindow == null || ganttChartWindow.IsClosed)
            {
                return false;
            }
            return true;
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

    }
}
