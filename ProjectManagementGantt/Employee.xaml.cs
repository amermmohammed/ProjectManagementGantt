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
        public Employee()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string firstName = firstNameTxt.Text;
            string lastname = lastNameTxt.Text;
            string dep = depTxt.Text;
            string tel = telTxt.Text;

            bool success = InsertEmployee(firstName, lastname, dep, tel);

            if (success)
            {
                firstNameTxt.Text = string.Empty;
                lastNameTxt.Text = string.Empty;
                depTxt.Text = string.Empty;
                telTxt.Text = string.Empty;
            }
        }

        public static bool InsertEmployee(string firstName, string lastName, string department, string tel)
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                string insertQuery = "INSERT INTO employees (firstname, lastname, department, tel) VALUES (@firstName, @lastName, @department, @tel);";

                using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@firstName", firstName);
                    cmd.Parameters.AddWithValue("@lastName", lastName);
                    cmd.Parameters.AddWithValue("@department", department);
                    cmd.Parameters.AddWithValue("@tel", tel);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmployeesWindow employeesWindow = new EmployeesWindow();
            employeesWindow.Show();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
