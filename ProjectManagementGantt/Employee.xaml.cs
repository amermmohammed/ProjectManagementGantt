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
using System.Data.SQLite;

namespace ProjectManagementGantt
{
    public partial class Employee : Window
    {
        private int employeeId = -1;
        public Employee()
        {
            InitializeComponent();
        }

        public Employee(string firstName, string lastName, string department, string tel, int employeeId = -1)
        {
            InitializeComponent();
            setData(firstName, lastName, department, tel, employeeId);
            this.employeeId = employeeId;

            //idLabel.Visibility = Visibility.Visible;
            //idTxt.Visibility = Visibility.Visible;
            idPanel.Visibility = Visibility.Visible;
        }

        private void setData(string firstName, string lastName, string department, string tel, int employeeId = -1)
        {
            idTxt.Text = employeeId.ToString();
            firstNameTxt.Text = firstName;
            lastNameTxt.Text = lastName;
            depTxt.Text = department;
            telTxt.Text = tel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameTxt.Text;
            string lastname = lastNameTxt.Text;
            string dep = depTxt.Text;
            string tel = telTxt.Text;
            
            bool success = InsertEmployee(firstName, lastname, dep, tel, this.employeeId);
            errorMessage.Text = "Error1";
            if (success)
            {
                setData(string.Empty, string.Empty, string.Empty, string.Empty);
                errorMessage.Text = "";
                MainWindow.employeeOverview.UpdateDataTable();
                this.Close();
            } else
            {
                errorMessage.Text = "Error: Bitte stellen Sie sicher, dass alle notwendigen Felder ausgefüllt sind.";
            }
        }

        public static bool InsertEmployee(string firstName, string lastName, string department, string tel, int employeeId = -1)
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sqlQuery;
                if (employeeId >= 0)
                {
                    sqlQuery = "UPDATE employees SET firstname = @firstName, lastname = @lastName, department = @department, tel = @tel WHERE id = @id;";
                } else
                {
                    sqlQuery = "INSERT INTO employees (firstname, lastname, department, tel) VALUES (@firstName, @lastName, @department, @tel);";
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, connection))
                {
                    if (firstName.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@firstName", firstName);
                    } 
                    if (lastName.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                    }
                    if (department.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@department", department);
                    }
                    if (tel.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@tel", tel);
                    }
                    if (employeeId >= 0)
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                    }
                    int rowsAffected = 0;
                    try
                    {
                       rowsAffected = cmd.ExecuteNonQuery();
                    } catch(Exception e)
                    {
                        MessageBox.Show("Error" + e);
                    }

                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }
    }
}
