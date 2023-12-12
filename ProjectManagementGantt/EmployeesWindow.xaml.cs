using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
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
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;
using System.Windows.Interop;


namespace ProjectManagementGantt
{
    /// <summary>
    /// Interaktionslogik für EmployeesWindow.xaml
    /// </summary>
    /// 


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

                string createTableQuery = "CREATE TABLE IF NOT EXISTS employees (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "firstname TEXT NOT NULL, " +
                    "lastname TEXT NOT NULL, " +
                    "department TEXT NOT NULL, " +
                    "tel TEXT NOT NULL);";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;

        }

        public static DataTable GetEmployees()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string selectQuery = "SELECT id as \"ID\", firstName as \"Vorname\", lastName as \"Name\", department as \"Abteilung\", tel as \"Telefon\" FROM employees;";
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

        /*
         String strConnection = Properties.Settings.Default.BooksConnectionString;
                SqlConnection con = new SqlConnection(strConnection);

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.Connection = con;
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.CommandText = "Select * from titles";
                SqlDataAdapter sqlDataAdap = new SqlDataAdapter(sqlCmd);

                DataTable dtRecord = new DataTable();
                sqlDataAdap.Fill(dtRecord);
                dataGridView1.DataSource = dtRecord;
         */
        public void UpdateDataTable()
        {
            DataTable newDataTable = GetEmployees();
            employeesDataGrid.ItemsSource = newDataTable.DefaultView;
            yourDataTable.Clear();
            yourDataTable.Merge(newDataTable);

            //UpdateTable();
        }
        /*
        private void UpdateTable()
        {
            TableRowGroup tableRowGroup = employeesTable.RowGroups[0];

            // Clear existing rows, except for the header row
            tableRowGroup.Rows.Clear();

            // Add header row
            TableRow headerRow = new TableRow();
            tableRowGroup.Rows.Add(headerRow);
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Nachname"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Vorname"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Abteilung"))));
            headerRow.Cells.Add(new TableCell(new Paragraph(new Run("Telefon"))));

            // Add rows based on data
            foreach (DataRow row in yourDataTable.Rows)
            {
                TableRow newRow = new TableRow();
                tableRowGroup.Rows.Add(newRow);

                newRow.Cells.Add(new TableCell(new Paragraph(new Run(row["lastname"].ToString()))));
                newRow.Cells.Add(new TableCell(new Paragraph(new Run(row["firstname"].ToString()))));
                newRow.Cells.Add(new TableCell(new Paragraph(new Run(row["department"].ToString()))));
                newRow.Cells.Add(new TableCell(new Paragraph(new Run(row["tel"].ToString()))));
            }
        }
        */
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }


        private void get_Click(object sender, RoutedEventArgs e)
        {

        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void editEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (employeesDataGrid.SelectedItem != null)
            {

                DataRowView selectedRow = employeesDataGrid.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    int employeeid = int.Parse(selectedRow.Row["ID"].ToString());
                    string name = selectedRow.Row["Name"].ToString();
                    string vorName = selectedRow.Row["Vorname"].ToString();
                    string abteilung = selectedRow.Row["Abteilung"].ToString();
                    string telefon = selectedRow.Row["Telefon"].ToString();
                    Employee employee = new Employee(name, vorName, abteilung, telefon, employeeid)
                    {
                        // Example: employee.Name = selectedRow.Row["Name"].ToString();
                    };

                    employee.Show();
                }
            }
            else
            {
                MessageBox.Show("No row selected");
            }
        }

        private void deleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (employeesDataGrid.SelectedItem != null)
            {

                DataRowView selectedRow = employeesDataGrid.SelectedItem as DataRowView;

                if (selectedRow != null)
                {
                    int employeeid = int.Parse(selectedRow.Row["ID"].ToString());
                    EmployeeDeleteWindow employeeToBeDeleted = new EmployeeDeleteWindow(employeeid);

                    employeeToBeDeleted.Show();
                }
            }
            else
            {
                MessageBox.Show("No row selected");
            }
        }
    }
}
