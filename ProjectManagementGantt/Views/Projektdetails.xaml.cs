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
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace ProjectManagementGantt
{
    /// <summary>
    /// Interaktionslogik für Projektdetails.xaml
    /// </summary>
    public partial class Projektdetails : Window
    {
        //##########-##########-##########-###########//

        private Project project;

        public Projektdetails()
        {
            InitializeComponent();
            LoadEmployees();
        }

        public Projektdetails(Project project)
        {
            InitializeComponent();
            LoadEmployees();
            this.project = project;
            setProjectData();
        }

        //##########-##########-##########-###########//

        public class Employee
        {
            public int Id { get; set; }
            public string Name { get; set; }
                                             
        }

        //##########-##########-##########-###########//

        private void setProjectData()
        {
            if (this.project != null)
            {
                descriptionText.Text = this.project.Title;
                startingDateText.Text = this.project.StartingDate;
                endingDateText.Text = this.project.EndingDate;
                //employeeComboBox.Text = "";
                //phaseNumberText.Text = phaseNumber;
                //phaseTitleText.Text = phaseTitle;
                //phaseDurationText.Text = phaseDuration;
                //phaseParentText.Text = phaseParent;
            }
        }

        private void setPhaseData(Phase phase)
        {
            if (this.project == null)
            {
                //employeeComboBox.Text = "";
                phaseNumberText.Text = phase.Number;
                phaseTitleText.Text = phase.Title;
                phaseDurationText.Text = phase.Duration.ToString();
                //phaseParentText.Text = phase.ParentPhaseId;
            } else
            {
                phaseNumberText.Text = string.Empty;
                phaseTitleText.Text = string.Empty;
                phaseDurationText.Text = string.Empty;
            }
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

            employeeComboBox.ItemsSource = employees;
            employeeComboBox.DisplayMemberPath = "Name";
            employeeComboBox.SelectedValuePath = "Id";
        }



        public static bool InsertProject(string title, string starting_date, string ending_date, int employee_id = -1, int project_id = -1)
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sqlQuery;
                if (project_id >= 0)
                {
                    sqlQuery = "UPDATE projects SET title = @title, starting_date = @starting_date, ending_date = @ending_date, employee_id = @employee_id WHERE id = @id;";
                }
                else
                {
                    sqlQuery = "INSERT INTO projects (title, starting_date, ending_date, employee_id) VALUES (@title, @starting_date, @ending_date, @employee_id);";
                }

                using (SQLiteCommand cmd = new SQLiteCommand(sqlQuery, connection))
                {
                    if (title.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                    }
                    if (starting_date.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@starting_date", starting_date);
                    }
                    if (ending_date.Length > 0)
                    {
                        cmd.Parameters.AddWithValue("@ending_date", ending_date);
                    }
                    if (employee_id >= 0)
                    {
                        cmd.Parameters.AddWithValue("@employee_id", employee_id);
                    }
                    if (project_id >= 0)
                    {
                        cmd.Parameters.AddWithValue("@id", project_id);
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
            // Speichern
            int projectId = -1;
            if (this.project != null)
            {
                projectId = this.project.Id;
                MessageBox.Show("Project Id set");
            }
            string title = descriptionText.Text;
            string starting_date = startingDateText.Text;
            string ending_date = endingDateText.Text;
            //MessageBox.Show("No user selected? " + employeeComboBox.SelectedValue.ToString());
            if (employeeComboBox.SelectedValue == null)
            {
                MessageBox.Show("No user selected");
                return;
            }
            //return;
            //MessageBox.Show("User selected?? " + employeeComboBox.SelectedValue.ToString());
            int employeeId = int.Parse(employeeComboBox.SelectedValue.ToString());

            bool success = InsertProject(title, starting_date, ending_date, employeeId, projectId);
            //errorMessage.Text = "Error1";
            if (success)
            {
                //setData(string.Empty, string.Empty, string.Empty, string.Empty);
                //errorMessage.Text = "";
                MainWindow.projectOverview.LoadProjectsinDataGrid();
                MainWindow.projectOverview.GetProjects();
                //this.Close();
            }
            else
            {
                //errorMessage.Text = "Error: Bitte stellen Sie sicher, dass alle notwendigen Felder ausgefüllt sind.";
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            // Phase Hinzufügen

        }
        //##########-##########-##########-###########//

    }
}
