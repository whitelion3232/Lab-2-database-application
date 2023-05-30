using System;
using System.Collections.Generic;
using System.Data.SqlClient;

public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class StudentRepository
{
    private string connectionString; // Connection string to the database

    public StudentRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public List<Student> GetAllStudents()
    {
        List<Student> students = new List<Student>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string query = "SELECT Id, Name FROM WBA.Students";
            SqlCommand command = new SqlCommand(query, connection);

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];

                    Student student = new Student
                    {
                        Id = id,
                        Name = name
                    };

                    students.Add(student);
                }
            }
        }

        return students;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=localhost;Initial Catalog=WBA_DatabaseConcepts;Trusted_Connection=SSPI;Encrypt=false;TrustServerCertificate=true;Integrated Security=True;"; // Replace with your actual connection string

        StudentRepository repository = new StudentRepository(connectionString);
        List<Student> students = repository.GetAllStudents();

        Console.WriteLine("List of Students:");
        foreach (Student student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
        }

        Console.ReadLine();
    }
}