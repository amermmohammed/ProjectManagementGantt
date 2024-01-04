using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ProjectManagement.ViewModels
{
    public class EmployeeDetailsViewModel
    {
        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private ProjectManagementContext Context;

        public event EventHandler OnRequestClose;

        public RelayCommand SaveEmployeeCommand { get; set; }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public Employee Employee;

        public bool IdVisibility { get; set; }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string Phone { get; set; }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public EmployeeDetailsViewModel(ProjectManagementContext Context)
        {
            SaveEmployeeCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
            this.Context = Context;
        }

        public EmployeeDetailsViewModel(ProjectManagementContext Context, Employee Employee)
        {
            SaveEmployeeCommand = new RelayCommand(SaveEmployee, CanSaveEmployee);
            this.Context = Context;
            this.Employee = Employee;
            updateWindowData();
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private void updateWindowData()
        {
            //MessageBox.Show("Update Window Data");
            if (Employee != null)
            {
                Id = Employee.Id;
                FirstName = Employee.FirstName;
                LastName = Employee.LastName;
                Department = Employee.Department;
                Phone = Employee.Phone;

                this.IdVisibility = true;
            } 
            else
            {
                this.IdVisibility = false;
            }
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool CanSaveEmployee(object obj)
        {
            /*
            if (string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Department) ||
                string.IsNullOrEmpty(Phone))
            {
                return false;
            }*/
            return true;
        }

        private void SaveEmployee(object obj)
        {
            if (string.IsNullOrEmpty(FirstName) ||
                string.IsNullOrEmpty(LastName) ||
                string.IsNullOrEmpty(Department) ||
                string.IsNullOrEmpty(Phone))
            {
                MessageBox.Show("Bitte füllen Sie alle nötigen Felder aus.");
                return;
            }

            if (Employee != null)
            {
                //MessageBox.Show("Update Employee");

                Employee.FirstName = FirstName;
                //MessageBox.Show("FN:" + FirstName);
                Employee.LastName = LastName;
                Employee.Department = Department;
                Employee.Phone = Phone;

                //Context.Update(Employee);
                

                //Employee.
                //Artists.Insert(index, artist);
                //Employees.Add(employee);
                //Context.Employees.Add(employee);
                Context.SaveChanges();
                //Context.Entry(Employee).Reload();

                //Employee employee = new Employee { FirstName = "Dummy", LastName = "Dummy", Department = "Dummy", Phone = "0000" };
                //Employees.Add(employee);
                //Context.Employees.Add(employee);

                // TODO Besseren Weg finden die Änderungen in der Tabelle anzuzeigen (neue elemente werden direkt angezeigt, veränderte elemente nicht)

                Context.Employees.Remove(Employee);
                Context.SaveChanges();

                Context.Employees.Add(Employee);
                Context.SaveChanges();

                //Context.Employees.Remove(employee);
                //Context.SaveChanges();
            } 
            else
            {
                //MessageBox.Show("Add New Employee");
                Employee employee = new Employee { 
                    FirstName = FirstName, 
                    LastName = LastName, 
                    Department = Department, 
                    Phone = Phone
                };

                Context.Employees.Add(employee);
                Context.SaveChanges();
                this.Employee = employee;
                //updateWindowData();
            }
            OnRequestClose(this, new EventArgs());
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

    }
}
