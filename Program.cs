using System;
using App.Data;

Console.WriteLine("Iniciando aplicação...");

// Cria o banco de dados se necessário
Database.CreateDatabase();

Console.WriteLine("Aplicação finalizada. Pressione Enter para sair...");
Console.ReadLine();

