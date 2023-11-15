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

                string createTableQuery = "CREATE TABLE IF NOT EXISTS projects (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "title TEXT, " +
                    "start_date TEXT, " +
                    "manager TEXT, " +
                "tel TEXT);";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;

        }

        public static DataTable GetProjects()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM projects;";
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

            dataGridView2.ItemsSource = dataView;
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


            if (dataGridView2.SelectedItem != null)
            {
                DataRowView selectedRow = (DataRowView)dataGridView2.SelectedItem;

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
