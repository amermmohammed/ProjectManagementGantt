using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ProjectManagement.ViewModels
{
    public class MainViewModel
    {
        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private static ProjectManagementContext Context;// = new ProjectManagementContext();

        private EmployeeList employeeListWindow;
        private ProjectList projectListWindow;

        public RelayCommand ShowEmployeeListWindowCommand { get; set; }
        public RelayCommand ShowProjectListWindowCommand { get; set; }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public MainViewModel()
        {
            Context = new ProjectManagementContext();
            Context.Employees.Load();
            Context.Projects.Load();
            Context.Phases.Load();

            //addSampleData();

            ShowEmployeeListWindowCommand = new RelayCommand(ShowEmployeeListWindow, CanShowEmployeeListWindow);
            ShowProjectListWindowCommand = new RelayCommand(ShowProjectListWindow, CanShowProjectListWindow);
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool CanShowEmployeeListWindow(object obj)
        {
            return true;
        }

        private void ShowEmployeeListWindow(object obj)
        {
            if (employeeListWindow == null || employeeListWindow.IsClosed)
            {
                employeeListWindow = new EmployeeList(Context);
                employeeListWindow.Show();
            }
            else
            {
                employeeListWindow.Show();
                employeeListWindow.Activate();
                if (employeeListWindow.WindowState == WindowState.Minimized)
                {
                    employeeListWindow.WindowState = WindowState.Normal;
                }
                //MessageBox.Show("Window already open.");
            }
            
        }

        private bool CanShowProjectListWindow(object obj)
        {
            return true;
        }

        private void ShowProjectListWindow(object obj)
        {
            if (projectListWindow == null || projectListWindow.IsClosed)
            {
                projectListWindow = new ProjectList(Context);
                projectListWindow.Show();
            }
            else
            {
                projectListWindow.Show();
                projectListWindow.Activate();
                if (projectListWindow.WindowState == WindowState.Minimized)
                {
                    projectListWindow.WindowState = WindowState.Normal;
                }
                //MessageBox.Show("Window already open.");
            }
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private void addSampleData()
        {
            Employee employee1 = new Employee { FirstName = "Steven", LastName = "King", Department = "AD_PRES", Phone = "xxxx" };
            Context.Employees.Add(new Employee { FirstName = "Alexander", LastName = "Hunold", Department = "IT_PROG", Phone = "xxxx" });
            Context.Employees.Add(new Employee { FirstName = "Ellen", LastName = "Abel", Department = "SA_REP", Phone = "xxxx" });
            Context.Employees.Add(new Employee { FirstName = "Wiliam", LastName = "Gietz", Department = "AC_ACCOUNT", Phone = "xxxx" });
            Employee employee2 = new Employee { FirstName = "Jennifer", LastName = "Whalen", Department = "AD_ASST", Phone = "xxxx" };
            Context.Employees.Add(employee2);
            Context.SaveChanges();

            Context.Projects.Add(new Project { Title = "Projekt 1", StartingDate = DateTime.Parse("13.09.2023").ToString(), EndingDate = DateTime.Parse("28.09.2023").ToString(), Employee = employee1 });
            Context.Projects.Add(new Project { Title = "Projekt 2", StartingDate = DateTime.Parse("11.11.2023").ToString(), EndingDate = DateTime.Parse("14.11.2023").ToString(), Employee = employee1 });
            Project project = new Project { Title = "Projekt 3", StartingDate = DateTime.Parse("10.01.2024").ToString(), EndingDate = DateTime.Parse("28.02.2024").ToString(), Employee = employee2 };
            Context.Projects.Add(project);
            Context.SaveChanges();

            Phase phase1 = new Phase { Number = "A", Title = "Phase Eins", Duration = 4, ParentPhase = null, Project = project };
            Phase phase2 = new Phase { Number = "B", Title = "Phase Zwei", Duration = 3, ParentPhase = phase1, Project = project };
            Phase phase3 = new Phase { Number = "C", Title = "Phase Drei", Duration = 6, ParentPhase = phase1, Project = project };
            Phase phase4 = new Phase { Number = "D", Title = "Phase Vier", Duration = 5, ParentPhase = phase2, Project = project };
            Phase phase5 = new Phase { Number = "E", Title = "Phase Fuenf", Duration = 3, ParentPhase = phase3, Project = project };
            Context.Phases.Add(phase1);
            Context.Phases.Add(phase2);
            Context.Phases.Add(phase3);
            Context.Phases.Add(phase4);
            Context.Phases.Add(phase5);
            Context.SaveChanges();
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

    }
}
