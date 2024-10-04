//using System;
//using System.Data; // For IDbConnection, IDbCommand, etc.
//using JDBC.NET.Data;
//using System.IO; // For handling file paths

//namespace OracleJdbcExample
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                // Path to the JAR file containing the Oracle JDBC driver
//                string oracleJarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "ojdbc8.jar");

//                // Check if the JAR file exists
//                if (!File.Exists(oracleJarPath))
//                {
//                    Console.WriteLine($"Oracle JDBC JAR file not found at {oracleJarPath}");
//                    return;
//                }

//                // Set up the JDBC connection string using JdbcConnectionStringBuilder
//                var oracleBuilder = new JdbcConnectionStringBuilder
//                {
//                    DriverPath = oracleJarPath, // Path to the Oracle JDBC JAR
//                    DriverClass = "oracle.jdbc.OracleDriver", // Fully qualified class name for Oracle JDBC driver
//                    JdbcUrl = "jdbc:oracle:thin:@//10.111.0.4:1521/VDMS6PDB1?user=system&password=vbd2024", // Full Oracle connection string with credentials
//                };

//                // Open the connection using JdbcConnection
//                using var connection = new JdbcConnection(oracleBuilder);
//                connection.Open();

//                Console.WriteLine("Connected to Oracle successfully!");

//                // Prepare a query to list all user tables
//                string query = "SELECT table_name FROM user_tables";

//                // Execute the query using ADO.NET style patterns
//                using var command = connection.CreateCommand();
//                command.CommandText = query;

//                // Execute the query and retrieve results
//                using var reader = command.ExecuteReader();

//                // Dynamically get the name of the columns in the result set
//                Console.WriteLine("Tables owned by the user:");
//                while (reader.Read())
//                {
//                    for (int i = 0; i < reader.FieldCount; i++)
//                    {
//                        var columnName = reader.GetName(i);
//                        var value = reader[i].ToString();
//                        Console.WriteLine($"{columnName}: {value}");
//                    }
//                }

//                // Close the connection when done
//                connection.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Error: {ex.Message}");
//            }
//        }
//    }
//}
