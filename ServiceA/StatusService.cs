using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceA
{
    public static class StatusService
    {
        private static string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=root;";

        public static async Task<List<Subdivision>> SelectAll()
        {
            var subdivisions = new List<Subdivision>();
            var connection = new NpgsqlConnection(connectionString);
            await connection.OpenAsync();
            var commandText = "SELECT * FROM subdivisions";

            using (NpgsqlCommand command = new NpgsqlCommand(commandText, connection))
            {
                using (NpgsqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        subdivisions.Add(new Subdivision(reader.GetInt32(0), reader.GetString(1),
                            reader.GetInt32(2) == 0 ? false : true,
                            Convert.IsDBNull(reader.GetValue(3)) ? null : (int?)reader.GetValue(3)));
                    }
                }
            }

            await connection.CloseAsync();
            return subdivisions;
        }
    }
}
