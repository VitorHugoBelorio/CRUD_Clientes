using DotNetEnv;
using Microsoft.Data.SqlClient;

namespace App.Data
{
    public static class Database
    {
        static Database()
        {
            Env.Load();
        }

        private static string BuildConnectionString()
        {
            var server = Environment.GetEnvironmentVariable("DB_SERVER") ?? "localhost,1433";
            var database = Environment.GetEnvironmentVariable("DB_NAME") ?? "CRUD_Clientes";
            var user = Environment.GetEnvironmentVariable("DB_USER") ?? "sa";
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD") ?? "";

            var trust = bool.TryParse(Environment.GetEnvironmentVariable("DB_TRUST_CERT"), out var t) ? t : true;
            var encrypt = bool.TryParse(Environment.GetEnvironmentVariable("DB_ENCRYPT"), out var e) ? e : true;

            var builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                UserID = user,
                Password = password,
                Encrypt = encrypt,
                TrustServerCertificate = trust
            };

            return builder.ConnectionString;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(BuildConnectionString());
        }

        public static void CreateDatabase()
        {
            try
            {
                // Conecta à base 'master' para poder criar o banco se ele não existir
                var masterBuilder = new SqlConnectionStringBuilder(BuildConnectionString()) { InitialCatalog = "master" };
                using var conn = new SqlConnection(masterBuilder.ConnectionString);
                conn.Open();

                var dbName = Environment.GetEnvironmentVariable("DB_NAME") ?? "CRUD_Clientes";
                var sql = $@"
                IF DB_ID(N'{dbName}') IS NULL
                BEGIN
                    CREATE DATABASE [{dbName}];
                END
                ";

                using var cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                Console.WriteLine($"Banco de dados '{dbName}' criado (ou já existente).");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Não foi possível gerar o banco de dados: {ex.Message}");
            }
        }
    }
}
