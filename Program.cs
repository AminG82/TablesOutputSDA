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
    IF NOT EXISTS (SELECT 1 FROM Employees WHERE Name = 'Alice')
        INSERT INTO Employees (Name, Position) VALUES ('Alice', 'Developer');
    IF NOT EXISTS (SELECT 1 FROM Employees WHERE Name = 'Bob')
        INSERT INTO Employees (Name, Position) VALUES ('Bob', 'Manager');
    IF NOT EXISTS (SELECT 1 FROM Employees WHERE Name = 'Charlie')
        INSERT INTO Employees (Name, Position) VALUES ('Charlie', 'Designer');
    """;

            string insertCustomers = """
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = 'David')
        INSERT INTO Customers (Name, Address) VALUES ('David', '123 Elm St');
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = 'Eve')
        INSERT INTO Customers (Name, Address) VALUES ('Eve', '456 Oak St');
    IF NOT EXISTS (SELECT 1 FROM Customers WHERE Name = 'Frank')
        INSERT INTO Customers (Name, Address) VALUES ('Frank', '789 Pine St');
    """;


            DataSet dataSet = new DataSet();
            SqlDataAdapter dataAdapterInsertEmployees = new SqlDataAdapter(insertEmployees, connection);
            SqlDataAdapter dataAdapterInsertCustomers = new SqlDataAdapter(insertCustomers, connection);

            SqlDataAdapter dataAdapterSelect = new SqlDataAdapter("""
                SELECT * FROM Employees; SELECT * FROM Customers;
                """, connection);

            dataAdapterInsertEmployees.Fill(dataSet);   // Only Run this one time to insert data into dataset
            dataAdapterInsertCustomers.Fill(dataSet);   // Only Run this one time to insert data into dataset


            dataAdapterSelect.Fill(dataSet);
            dataSet.Tables[0].TableName = "Employees";
            dataSet.Tables[1].TableName = "Customers";
            Thread.Sleep(1000); // Wait for the table to be created
            foreach (DataRow row in dataSet.Tables["Employees"].Rows)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["Position"]}");
            }
            Console.WriteLine("--------------------------------------------------");
            foreach (DataRow row in dataSet.Tables["Customers"].Rows)
            {
                Console.WriteLine($"{row["Id"]}, {row["Name"]}, {row["Address"]}");
            }
        }
    }
}
