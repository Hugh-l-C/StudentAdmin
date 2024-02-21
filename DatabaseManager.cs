using Newtonsoft.Json;
using System.Data.SQLite;
using System.Collections.Generic;

public class DatabaseManager
{
    private readonly string connectionString;

    public DatabaseManager(string dbPath)
    {
        connectionString = $"Data Source={dbPath};Version=3;";
        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            var checkTableCmd = connection.CreateCommand();
            checkTableCmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='SerializedStudents';";
            var result = checkTableCmd.ExecuteScalar();

            if (result == null)
            {
                var createTableCmd = connection.CreateCommand();
                createTableCmd.CommandText = @"
                    CREATE TABLE SerializedStudents (
                        ID INTEGER PRIMARY KEY,
                        Data TEXT NOT NULL
                    );";
                createTableCmd.ExecuteNonQuery();
            }
        }
    }

    public void SaveStudent(Student student)
    {
        var serializedData = JsonConvert.SerializeObject(student);

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("INSERT OR REPLACE INTO SerializedStudents (ID, Data) VALUES (@ID, @Data)", connection))
            {
                command.Parameters.AddWithValue("@ID", student.ID);
                command.Parameters.AddWithValue("@Data", serializedData);
                command.ExecuteNonQuery();
            }
            connection.Close();
        }
    }

    public Student GetStudent(int id)
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            object result;
            connection.Open();
            using (SQLiteCommand command = new SQLiteCommand("SELECT Data FROM SerializedStudents WHERE ID = @ID", connection))
            {
                command.Parameters.AddWithValue("@ID", id);
                result = command.ExecuteScalar();
            }
            connection.Close();
            return result != null ? JsonConvert.DeserializeObject<Student>(result.ToString()) : null;
        }
    }

    public List<Student> GetAllStudents()
    {
        var students = new List<Student>();

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand("SELECT Data FROM SerializedStudents", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var serializedData = reader.GetString(0);
                    var student = JsonConvert.DeserializeObject<Student>(serializedData);
                    if (student != null)
                    {
                        students.Add(student);
                    }
                }
            }
            connection.Close();
        }
        return students;
    }
}