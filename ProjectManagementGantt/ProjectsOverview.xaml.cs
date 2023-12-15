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
    /// Interaktionslogik für ProjectOverview.xaml
    /// </summary>
    
    public partial class ProjectOverview : Window
    {
        public ProjectOverview()
        {
            InitializeComponent();
            CreateProjectsTable();
            InsertFakeDataIntoProjects(5);
            LoadProjectsData();
        }

        public static bool CreateProjectsTable()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS projects (
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    title TEXT NOT NULL, 
                    starting_date TEXT NOT NULL, 
                    ending_date TEXT NOT NULL, 
                    employee_id INTEGER,
                    FOREIGN KEY(employee_id) REFERENCES employees(id) 
                    ON DELETE CASCADE ON UPDATE CASCADE
                );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;
        }

    public static void InsertFakeDataIntoProjects(int numberOfProjects)
        {
            string connectionString = "Data Source=db.db;Version=3;";
            Random random = new Random();

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < numberOfProjects; i++)
                {
                    // Generate fake data
                    string title = "Project " + Guid.NewGuid().ToString();
                    string startingDate = RandomDay(random).ToString("yyyy-MM-dd");
                    string endingDate = RandomDay(random, true).ToString("yyyy-MM-dd");
                    int employeeId = random.Next(1, 10); // Assuming you have employee IDs between 1 and 10

                    string insertQuery = "INSERT INTO projects (title, starting_date, ending_date, employee_id) VALUES (@title, @startingDate, @endingDate, @employeeId)";

                    using (SQLiteCommand cmd = new SQLiteCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@title", title);
                        cmd.Parameters.AddWithValue("@startingDate", startingDate);
                        cmd.Parameters.AddWithValue("@endingDate", endingDate);
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);

                        cmd.ExecuteNonQuery();
                    }
                }

                connection.Close();
            }
        }

        private static DateTime RandomDay(Random random, bool futureDate = false)
        {
            DateTime start = futureDate ? DateTime.Today : new DateTime(1995, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(random.Next(range));
        }


        private void LoadProjectsData()
        {
            projectsDataGrid.ItemsSource = GetProjects();
        }

        private List<Project> GetProjects()
        {
            var projects = new List<Project>();
            string connectionString = "Data Source=db.db;Version=3;";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM projects";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            projects.Add(new Project
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Title = reader["title"].ToString(),
                                StartingDate = reader["starting_date"].ToString(),
                                EndingDate = reader["ending_date"].ToString(),
                                EmployeeId = Convert.ToInt32(reader["employee_id"])
                            });
                        }
                    }
                }
                connection.Close();
            }
            return projects;
        }

        // Definition of the Project class inside ProjectOverview
        private class Project
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string StartingDate { get; set; }
            public string EndingDate { get; set; }
            public int EmployeeId { get; set; }
        }

        private void projectsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Projektdetails projektdetails = new Projektdetails();
            projektdetails.Show();
        }
    }
}
