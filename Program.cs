using DotNetEnv;
using Microsoft.Data.SqlClient;

Env.Load();

string connectionString =
    $"Server={Environment.GetEnvironmentVariable("DB_SERVER")};" +
    $"Database={Environment.GetEnvironmentVariable("DB_NAME")};" +
    $"User Id={Environment.GetEnvironmentVariable("DB_USER")};" +
    $"Password={Environment.GetEnvironmentVariable("DB_PASSWORD")};" +
    $"TrustServerCertificate={Environment.GetEnvironmentVariable("DB_TRUST_CERT")};";
