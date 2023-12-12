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
    /// Interaktionslogik für EmployeeDeleteWindow.xaml
    /// </summary>
    public partial class EmployeeDeleteWindow : Window
    {
        private int employeeId;
        public EmployeeDeleteWindow(int employeeId)
        {
            InitializeComponent();
            this.employeeId = employeeId;
        }

        private bool DeleteEmpolyee(int employeeId)
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sqlQuery;
                sqlQuery = "DELETE FROM employees WHERE id = @id;";

                using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, connection))
                {
                    if (employeeId >= 0)
                    {
                        cmd.Parameters.AddWithValue("@id", employeeId);
                    }
                    int rowsAffected = 0;
                    try
                    {
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Error" + e);
                    }

                    connection.Close();

                    return rowsAffected > 0;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DeleteEmpolyee(this.employeeId);
            MainWindow.employeesWindow.UpdateDataTable();
            this.Close();
        }
    }
}
