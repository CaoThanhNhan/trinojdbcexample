using System;
using System.Data; // For IDbConnection, IDbCommand, etc.
using JDBC.NET.Data;
using System.IO;
using System.Collections.Generic;

namespace TrinoJdbcExample
{
    class Program
    {
        static void Main()
        {
                // Path containing the Trino JDBC driver
                string trinoJarPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib", "trino-jdbc-459.jar");

                if (!File.Exists(trinoJarPath))
                {
                    Console.WriteLine($"Trino JDBC JAR file not found at {trinoJarPath}");
                    return;
                }

                var trinoBuilder = new JdbcConnectionStringBuilder
                {
                    DriverPath = trinoJarPath,
                    DriverClass = "io.trino.jdbc.TrinoDriver",
                    JdbcUrl = "jdbc:trino://10.225.0.32:8090", // Trino host and port
                };

                var props = new Dictionary<string, string>
                {
                    { "user", "root" },
                    { "SSL", "false" }  
                };

                using var connection = new JdbcConnection(trinoBuilder, props);
                connection.Open();

                Console.WriteLine("Connected to Trino successfully!");

                // Query to join data from MongoDB and Oracle
                string query = @"SELECT ROW_NUMBER() OVER() AS row_index, c.c_custkey, o.lo_orderkey, o.lo_orderdate
                                    FROM oracle.system.customer c
                                    JOIN mongodb.TNG_LISP.testPerformanceTrino o
                                    ON c.c_custkey = o.lo_custkey
                                    WHERE o.lo_orderdate BETWEEN 19960101 AND 19961231
                                  ";


                using var command = connection.CreateCommand();
                command.CommandText = query;

                // Execute the query and retrieve results
                using var reader = command.ExecuteReader();

                // Dynamically print columns
                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        var columnName = reader.GetName(i);
                        var value = reader[i].ToString();
                        Console.WriteLine($"{columnName}: {value}");
                    }
                }

                // Close the connection
                connection.Close();
        }
    }
}
