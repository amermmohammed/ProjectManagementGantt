using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ProjectManagement.ViewModels
{
    public class ProjectDetailsViewModel: INotifyPropertyChanged
    {
        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private ProjectManagementContext Context;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public RelayCommand SaveProjectCommand { get; set; }
        public RelayCommand SavePhaseCommand { get; set; }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public Project Project;
        public ObservableCollection<Employee> Employees { get; set; }
        //public List<Employee> Employees2 { get; set; }

        private int _Id;
        public int Id {
            get => _Id;
            set
            {
                if (value == _Id) return;
                _Id = value;
                OnPropertyChanged();
            }
        }
        private string _Title;
        public string Title {
            get => _Title;
            set
            {
                if (value == _Title) return;
                _Title = value;
                OnPropertyChanged();
            }
        }
        private DateTime _StartingDate;
        public DateTime StartingDate {
            get => _StartingDate;
            set
            {
                if (value == _StartingDate) return;
                _StartingDate = value;
                OnPropertyChanged();
            }
        }
        private DateTime _EndingDate;
        public DateTime EndingDate {
            get => _EndingDate;
            set
            {
                if (value == _EndingDate) return;
                _EndingDate = value;
                OnPropertyChanged();
            }
        }
        private Employee _Employee;
        public Employee Employee {
            get => _Employee;
            set
            {
                if (value == _Employee) return;
                _Employee = value;
                OnPropertyChanged();
            }
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private List<Phase> _Phases;
        public List<Phase> Phases
        {
            get => _Phases;
            set
            {
                if (value == _Phases) return;
                _Phases = value;
                OnPropertyChanged();
            }
        }

        private bool _PhaseVisibility;
        public bool PhaseVisibility
        {
            get => _PhaseVisibility;
            set
            {
                if (value == _PhaseVisibility) return;
                _PhaseVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _PhaseNumber;
        public string PhaseNumber {
            get => _PhaseNumber;
            set
            {
                if (value == _PhaseNumber) return;
                _PhaseNumber = value;
                OnPropertyChanged();
            }
        }
        private string _PhaseTitle;
        public string PhaseTitle {
            get => _PhaseTitle;
            set
            {
                if (value == _PhaseTitle) return;
                _PhaseTitle = value;
                OnPropertyChanged();
            }
        }
        private int _PhaseDuration;
        public int PhaseDuration {
            get => _PhaseDuration;
            set
            {
                if (value == _PhaseDuration) return;
                _PhaseDuration = value;
                OnPropertyChanged();
            }
        }
        private Phase _PhaseParentPhase;
        public Phase PhaseParentPhase {
            get => _PhaseParentPhase;
            set
            {
                if (value == _PhaseParentPhase) return;
                _PhaseParentPhase = value;
                OnPropertyChanged();
            }
        }
        private Phase _Phase;
        public Phase Phase {
            get => _Phase;
            set
            {
                if (value == _Phase) return;
                _Phase = value;
                OnPropertyChanged();
            }
        } // "selected" Phase

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public ProjectDetailsViewModel(ProjectManagementContext Context)
        {
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            SavePhaseCommand = new RelayCommand(SavePhase, CanSavePhase);
            this.Context = Context;
            Employees = Context.Employees.Local.ToObservableCollection();
            updateWindowProjectData();
            //Context.Phases.Where(b => b.Name == "TEST").Load();
            //Employees = Context.Employees.Local.ToObservableCollection();
            //Employees2 = Context.Employees.Local.ToList();
            /*
             * SelectedValuePath="Id"
                          SelectedValue="{Binding Employee}"
             */
        }

        public ProjectDetailsViewModel(ProjectManagementContext Context, Project Project)
        {
            SaveProjectCommand = new RelayCommand(SaveProject, CanSaveProject);
            SavePhaseCommand = new RelayCommand(SavePhase, CanSavePhase);
            this.Context = Context;
            Employees = Context.Employees.Local.ToObservableCollection();
            this.Project = Project;
            updateWindowProjectData();
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool CanSaveProject(object obj)
        {
            return true;
        }

        private void SaveProject(object obj)
        {
            if (string.IsNullOrEmpty(Title) ||
                Employee == null ||
                StartingDate == null ||
                EndingDate == null)
            {
                MessageBox.Show("Bitte füllen Sie alle nötigen Felder aus.");
                return;
            }

            if (EndingDate < StartingDate)
            {
                MessageBox.Show("Das Enddatum darf nicht früher als das Startdatum sein.");
                return;
            }

            if (Project != null)
            {
                Project.Title = Title;
                Project.StartingDate = StartingDate.ToString();
                Project.EndingDate = EndingDate.ToString();
                Project.Employee = Employee;

                Context.Projects.Remove(Project);
                Context.SaveChanges();
                Context.Projects.Add(Project);
                Context.SaveChanges();
            }
            else
            {
                Project project = new Project
                {
                    Title = Title,
                    StartingDate = StartingDate.ToString(),
                    EndingDate = EndingDate.ToString(),
                    Employee = Employee
                };
                Context.Projects.Add(project);
                Context.SaveChanges();
                this.Project = project;
            }
            updateWindowProjectData();
        }

        private bool CanSavePhase(object obj)
        {
            return true;
        }

        private void SavePhase(object obj)
        {
            if (string.IsNullOrEmpty(PhaseNumber) ||
                string.IsNullOrEmpty(PhaseTitle) ||
                StartingDate == null ||
                EndingDate == null)
            {
                MessageBox.Show("Bitte füllen Sie alle nötigen Felder der Phase aus.");
                return;
            }
            if (PhaseDuration < 1)
            {
                MessageBox.Show("Das Dauer muss mindestens 1 sein.");
                return;
            }

            //MessageBox.Show("Save Phase");
            if (Phase != null)
            {
                Phase.Number = PhaseNumber;
                Phase.Title = PhaseTitle;
                Phase.Duration = PhaseDuration;
                Phase.ParentPhase = PhaseParentPhase;
                /*
                Context.Phases.Remove(Phase);
                Context.SaveChanges();

                Context.Phases.Add(Phase);
                Context.SaveChanges();
                */
            }
            else
            {
                Phase phase = new Phase
                {
                    Number = PhaseNumber,
                    Title = PhaseTitle,
                    Duration = PhaseDuration,
                    ParentPhase = PhaseParentPhase,
                    Project = Project
                };
                Context.Phases.Add(phase);
                Context.SaveChanges();
                this.Phase = null;
            }
            updateWindowPhaseData();
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private void updateWindowProjectData()
        {
            if (Project != null)
            {
                Id = Project.Id;
                Title = Project.Title;
                StartingDate = DateTime.Parse(Project.StartingDate); 
                EndingDate = DateTime.Parse(Project.EndingDate);
                Employee = Project.Employee;

                updateWindowPhaseData();
                this.PhaseVisibility = true;
            }
            else
            {
                //Title = "TEST 1";
                //StartingDate = DateTime.Parse("13.09.2023");
                this.PhaseVisibility = false;
                StartingDate = DateTime.Today;
                EndingDate = DateTime.Today.AddDays(1);
                
                //MyDate = new DateTime(2009, 3, 23);
            }
        }

        private void updateWindowPhaseData()
        {
            if (Project == null)
            {
                return;
            }
            if (Project.Phases != null)
            {
                Phases = Project.Phases.ToList();
            }
            //Phases = Project.Phases.ToList();

            if (Phase != null)
            {
                //MessageBox.Show("Set Phase Window");
                PhaseNumber = Phase.Number;
                PhaseTitle = Phase.Title;
                PhaseDuration = Phase.Duration;
                PhaseParentPhase = Phase.ParentPhase;
            }
            else
            {
                //MessageBox.Show("Clear Phase Window");
                PhaseNumber = "";
                PhaseTitle = "";
                PhaseDuration = 0;
                PhaseParentPhase = null;
            }
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

    }
}
