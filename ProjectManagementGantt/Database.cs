using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace ProjectManagementGantt
{
    internal class Database
    {

        public static bool CreateEmployeesTable()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS employees (
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    firstname TEXT NOT NULL, 
                    lastname TEXT NOT NULL, 
                    department TEXT NOT NULL, 
                    tel TEXT NOT NULL
                );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            return true;
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

        public static bool CreatePhaseTable()
        {
            string connectionString = "Data Source=db.db;Version=3;";
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS phases (
                    id INTEGER PRIMARY KEY AUTOINCREMENT, 
                    number TEXT NOT NULL, 
                    title TEXT NOT NULL, 
                    duration INTEGER NOT NULL, 
                    project_id INTEGER NOT NULL,
                    parent_phase_id INTEGER,
                    
                    FOREIGN KEY(project_id) REFERENCES projects(id) ON DELETE CASCADE ON UPDATE CASCADE
                    FOREIGN KEY(parent_phase_id) REFERENCES phases(id) ON DELETE CASCADE ON UPDATE CASCADE
                );";

                using (SQLiteCommand cmd = new SQLiteCommand(createTableQuery, connection))
                {
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }

            return true;
        }


    }
}
