using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApplication
{
    public static class StatusService
    {
        public static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root;";

        public static void EnsureCreated(string connectionString)
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            var commandText = "SELECT table_name FROM information_schema.tables WHERE table_schema = 'public'";
            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader.GetValue(0).ToString() == "subdivisions")
                        {
                            return;
                        }
                    }
                }    
            }
            var createTableText = "CREATE TABLE subdivisions(id SERIAL NOT NULL PRIMARY KEY, name VARCHAR(255) UNIQUE, " +
                        "status SMALLINT, ownerid INT)";
            using (NpgsqlCommand createCommand = new NpgsqlCommand(createTableText, connection))
            {
                createCommand.ExecuteScalar();
            }
            connection.Close();
        }


        private static async Task UpdateSubdivision(ActualStatusSubdivision subdivision, NpgsqlConnection connection)
        {
            var commandText = $"UPDATE subdivisions SET status = '{Convert.ToInt32(subdivision.Status)}' WHERE name = '{subdivision.Name}'";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }

        public static async Task<List<ActualStatusSubdivision>> SelectAll()
        {
            var subdivisions = new List<ActualStatusSubdivision>();
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            var commandText = "SELECT * FROM subdivisions";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        subdivisions.Add(new ActualStatusSubdivision(reader.GetInt32(0), reader.GetString(1), 
                            reader.GetInt32(2) == 0 ? false: true,
                            Convert.IsDBNull(reader.GetValue(3)) ? null : (int?)reader.GetValue(3)));
                    }
                }
            }

            await connection.CloseAsync();
            return subdivisions;
        }

        public static async Task ChangeStatus(List<ActualStatusSubdivision> subdivisions)
        {
            await Task.Delay(3000);
            subdivisions.ForEach(x => {
                if (x.Status)
                {
                    x.Status = false;
                }
                else
                {
                    x.Status = true;
                }
            });

            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            foreach (var item in subdivisions)
            {
                await UpdateSubdivision(item, connection);
            }
            await connection.CloseAsync();
        }
    }
}
