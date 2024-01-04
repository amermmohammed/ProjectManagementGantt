using Microsoft.EntityFrameworkCore;
using ProjectManagement.DAL;
using ProjectManagement.Models;
using ProjectManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;

namespace ProjectManagement.ViewModels
{
    public class EmployeeListViewModel
    {
        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private ProjectManagementContext Context;

        public ObservableCollection<Employee> Employees { get; set; }

        private EmployeeDetails employeeDetailsWindow;

        public RelayCommand ShowEmployeeDetailsWindowCommand { get; set; }
        public RelayCommand DeleteSelectedEmployeeCommand { get; set; }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        public EmployeeListViewModel(ProjectManagementContext Context)
        {
            //Context.Employees.Load();
            //Employees = new ObservableCollection<Employee>(Context.Employees.ToList() as List<Employee>);
            //Employees = new ObservableCollection<Employee>(Context.Employees);
            this.Context = Context;
            Context.Employees.Load();
            Employees = Context.Employees.Local.ToObservableCollection();

            ShowEmployeeDetailsWindowCommand = new RelayCommand(ShowEmployeeDetailsWindow, CanShowEmployeeDetailsWindow);
            DeleteSelectedEmployeeCommand = new RelayCommand(DeleteSelectedEmployee, CanDeleteSelectedEmployee);
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool CanDeleteSelectedEmployee(object obj)
        {
            return true;
        }

        private void DeleteSelectedEmployee(object obj)
        {
            if (isEmployeeDetailsWindowOpen())
            {
                MessageBox.Show("Bitte schließen Sie die offenen Mitarbeiter Fenster um ein neues zu öffnen.");
                return;
            }

            if (obj is Employee employee)
            {
                MessageBoxResult resultMessageBox = MessageBox.Show(
                    "Möchten Sie den ausgewählten Mitarbeiter wirklich löschen?\n\nId: " + employee.Id + " \nVorname: " + employee.FirstName + " \nNachname: " + employee.LastName,
                    "Info", 
                    MessageBoxButton.YesNo,
                    MessageBoxImage.None);

                switch (resultMessageBox)
                {
                    case MessageBoxResult.Yes:
                        Context.Employees.Remove(employee);
                        Context.SaveChanges();
                        break;

                    case MessageBoxResult.No:
                        /* ... */
                        break;
                }
            }
            else
            {
                MessageBox.Show("No row selected");
            }

        }

        private bool CanShowEmployeeDetailsWindow(object obj)
        {
            return true;
        }

        private void ShowEmployeeDetailsWindow(object obj)
        {
            if (isEmployeeDetailsWindowOpen())
            {
                MessageBox.Show("Bitte schließen Sie die offenen Mitarbeiter Fenster um ein neues zu öffnen.");
                return;
            }

            if (obj is String name && name == "New") {
                //MessageBox.Show("NEW NEW");
                employeeDetailsWindow = new EmployeeDetails(Context);
                employeeDetailsWindow.Show();
            } 
            else
            {
                if (obj is Employee employee)
                {
                    //MessageBox.Show("ID: " + employee.Id);
                    //employee.FirstName = employee.FirstName + "_SELECTED";
                    //Context.SaveChanges();
                    employeeDetailsWindow = new EmployeeDetails(Context, employee);
                    employeeDetailsWindow.Show();
                }
                else
                {
                    MessageBox.Show("No row selected");
                }
            }
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        private bool isEmployeeDetailsWindowOpen()
        {
            /*
            if (employeeDetailsWindow != null && !employeeDetailsWindow.IsClosed)
            {
                if (!employeeDetailsWindow.IsClosed)
                {
                    return true;
                }
            }
            return false
            */

            if (employeeDetailsWindow == null || employeeDetailsWindow.IsClosed)
            {
                return false;
            }
            return true;
        }

        //#---#---#---#---#---#---#---#---#---#---#---#---#---#---#---#

        /*
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }
        */

    }
}
