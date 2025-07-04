using Microsoft.Data.SqlClient;

namespace TablesOutputSDA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Create a new table using SqlCommand , Insert Data and Select using SqlDataAdapter

            string connectionString = """
                Data Source =.; Initial Catalog = TestDB; User ID = sa; Encrypt = False
                """;

            SqlConnection connection = new SqlConnection(connectionString);
        }
    }
}
