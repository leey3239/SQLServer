using Microsoft.Data.SqlClient;
using System.Data;

namespace ConsoleApp5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                const string ConnectionString = "Data Source=Komp3wtah;Initial Catalog=StudentOption;Integrated Security=True;TrustServerCertificate=True";
                foreach (string str in GetStudents(ConnectionString))
                {
                    Console.WriteLine(str);
                }
                AddStudent(ConnectionString);
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nDone. Press enter.");
            Console.ReadLine();
        }
        public static List<string> GetStudents(string _connectionString)
        {
            var students = new List<string>();
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Student";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string student = dataReader.GetString(1) + dataReader.GetString(2);
                    students.Add(student);
                }
            }
            return students;
        }

        public static void AddStudent(string _connectionString)
        {
            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqlCommand command1 = connection.CreateCommand();
                command1.CommandText = "SELECT StudentID FROM Student";
                var dataReader = command1.ExecuteReader();
                int studentID = 0;
                while (dataReader.Read())
                {
                    studentID = dataReader.GetInt32(0);
                }

                SqlCommand command = connection.CreateCommand();
                Console.WriteLine("Input First Name");
                string firstName = Console.ReadLine();
                Console.WriteLine("Input Second Name");
                string secondName = Console.ReadLine();
                Console.WriteLine("Input DoB");
                string dateOfBirth = Console.ReadLine();
                command.CommandText = $"INSERT INTO dbo.student VALUES ({studentID+1}, {firstName}, {secondName}, {dateOfBirth})";
                command.ExecuteNonQuery();
            }
        }
    }
}