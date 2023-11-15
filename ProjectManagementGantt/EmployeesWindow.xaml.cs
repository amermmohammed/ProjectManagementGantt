using System;
//using System.Windows.Forms;
using System.Data.SQLite;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.ComponentModel;

namespace ProjectManagementGantt
{
    public partial class EmployeesWindow : Window
    {
        private DataTable yourDataTable = new DataTable();
        public EmployeesWindow()
        {
            InitializeComponent();
            crateDb();
            UpdateDataTable();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee();
            employee.Show();
        }

        public static bool crateDb()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                // Existing employees table
                string createEmployeesTableQuery = @"CREATE TABLE IF NOT EXISTS employees (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            firstname TEXT,
            lastname TEXT,
            department TEXT,
            tel TEXT
        );";
                ExecuteNonQuery(connection, createEmployeesTableQuery);

                // New projects table
                string createProjectsTableQuery = @"CREATE TABLE IF NOT EXISTS projects (
            title TEXT PRIMARY KEY,
            startdate TEXT,
            enddate TEXT,
            owner INTEGER,
            FOREIGN KEY(owner) REFERENCES employees(id)
        );";
                ExecuteNonQuery(connection, createProjectsTableQuery);

                // New project_phases table
                string createProjectPhasesTableQuery = @"CREATE TABLE IF NOT EXISTS project_phases (
            number INTEGER PRIMARY KEY AUTOINCREMENT,
            phase TEXT,
            duration INTEGER,
            predecessor INTEGER,
            FOREIGN KEY(predecessor) REFERENCES project_phases(number)
        );";
                ExecuteNonQuery(connection, createProjectPhasesTableQuery);

                connection.Close();
            }

            return true;

        }

        private static void ExecuteNonQuery(SQLiteConnection connection, string query)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(query, connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public static DataTable GetEmployees()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM employees;";
                using (SQLiteCommand cmd = new SQLiteCommand(selectQuery, connection))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
            }
        }

        public void UpdateDataTable()
        {
            DataTable newDataTable = GetEmployees();

            yourDataTable.Clear();
            yourDataTable.Merge(newDataTable);

            ICollectionView dataView = CollectionViewSource.GetDefaultView(yourDataTable);

            dataGridView1.ItemsSource = dataView;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void get_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {


       
            if (dataGridView1.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGridView1.SelectedItem;
)
                int employeeId = (int)selectedRow["id"];

                if (DeleteEmployeeFromDatabase(employeeId))
                {
                    UpdateDataTable();
                }
                else
                {
                    MessageBox.Show("Fehler beim Löschen des Mitarbeiters.");
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie einen Mitarbeiter zum Löschen aus.");
            }
        }

        private bool DeleteEmployeeFromDatabase(int employeeId)
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string deleteQuery = "DELETE FROM employees WHERE id = @id;";

                using (SQLiteCommand cmd = new SQLiteCommand(deleteQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@id", employeeId);
                    int rowsAffected = cmd.ExecuteNonQuery();

                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }

    }
}
