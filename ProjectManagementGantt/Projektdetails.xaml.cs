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
    /// <summary>
    /// Interaktionslogik für Projektdetails.xaml
    /// </summary>
    public partial class Projektdetails : Window
    {
        public Projektdetails()
        {
            InitializeComponent();
            LoadEmployees();
        }

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
                                             
        }

        private void LoadEmployees()
        {
            var employees = new List<Employee>();
            string connectionString = "Data Source=db.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id, firstname, lastname FROM employees";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            employees.Add(new Employee
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Name = $"{reader["firstname"]} {reader["lastname"]}"
                            });
                        }
                    }
                }
                connection.Close();
            }

            OwnerComboBox.ItemsSource = employees;
            OwnerComboBox.DisplayMemberPath = "Name";
            OwnerComboBox.SelectedValuePath = "Id";
        }


    }
}
