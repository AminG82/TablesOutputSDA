using Microsoft.Data.SqlClient;
using System.Data;

namespace TablesOutputSDA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new table using SqlCommand , Insert Data and Select using SqlDataAdapter

            string connectionString = """
                Data Source =.; Initial Catalog = TestDB; User ID = sa; Password = amin5123 ; Encrypt = False
                """;
            SqlConnection connection = new SqlConnection(connectionString);
            

            //SqlCommand CreateTableEmployees = new SqlCommand("""
            //    CREATE TABLE Employees (
            //        Id INT PRIMARY KEY IDENTITY(1,1),
            //        Name NVARCHAR(20),
            //        Position NVARCHAR(50)
            //    );
            //    """, connection);

            //SqlCommand CreateTableCustomers = new SqlCommand("""
            //    CREATE TABLE Customers (
            //        Id INT PRIMARY KEY IDENTITY(1,1),
            //        Name NVARCHAR(20),
            //        Address NVARCHAR(100)
            //    );
            //    """, connection);

            //connection.Open();
            //Console.WriteLine(CreateTableEmployees.ExecuteNonQuery());
            //Console.WriteLine(CreateTableCustomers.ExecuteNonQuery());
            //connection.Close();

            string insertEmployees = """
                INSERT INTO Employees (Name, Position) VALUES
                ('Alice', 'Developer'),
                ('Bob', 'Manager'),
                ('Charlie', 'Designer');
                """;

            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapterInsertEmployees = new SqlDataAdapter(insertEmployees , connection);
            
            SqlDataAdapter dataAdapterSelectEmployees = new SqlDataAdapter("SELECT * FROM Employees", connection);
            dataAdapterSelectEmployees.Fill(dataSet, "Employees");
            Thread.Sleep(1000); // Wait for the table to be created
            foreach (DataRow row in dataSet.Tables["Employees"].Rows)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["Position"]}");
            }   
        }
    }
}
