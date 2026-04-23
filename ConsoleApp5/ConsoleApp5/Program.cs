using Microsoft.Data.Sqlite;
using System;
using System.IO;

class DatabaseManager
{
    private const string ConnectionString = "Data Source=CreativeStudio.db";
    private const string SqlScriptPath = "C:\\Users\\admin.ACADEMY\\RiderProjects\\ConsoleApp5\\ConsoleApp5\\DataBase\\Hill.sql";

    public static void InitializeDatabase()
    {
        try
        {

            if (!File.Exists(SqlScriptPath))
            {
                Console.WriteLine($"Ошибка: Файл {SqlScriptPath} не найден!");
                return;
            }

            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string sqlScript = File.ReadAllText(SqlScriptPath);

            string[] commands = sqlScript.Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string commandText in commands)
            {
                using var command = connection.CreateCommand();
                command.CommandText = commandText.Trim();
                if (!string.IsNullOrEmpty(command.CommandText))
                {
                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine("База данных успешно инициализирована!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при инициализации базы данных: {ex.Message}");
        }
    }
    
    public static void DisplayProjectParticipants()
    {
        try
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            var query = @"
                SELECT
                    p.Name AS ProjectName,
                    p.Deadline,
                    pr.Nickname,
                    pp.Role,
                    pf.PassportData,
                    pf.Address
                FROM Projects p
                JOIN ProjectParticipants pp ON p.Id = pp.ProjectId
                JOIN Performers pr ON pp.PerformerId = pr.Id
                JOIN PersonalFiles pf ON pr.Id = pf.PerformerId
                ORDER BY p.Name, pr.Nickname";

            using var command = new SqliteCommand(query, connection);
            using var reader = command.ExecuteReader();

            string currentProject = null;

            while (reader.Read())
            {
                string projectName = reader.GetString(0);
                string deadline = reader.GetString(1);
                string nickname = reader.GetString(2);
                string role = reader.GetString(3);
                string passportData = reader.GetString(4);
                string address = reader.GetString(5);
                
                if (currentProject != projectName)
                {
                    Console.WriteLine($"\nПроект: {projectName}");
                    Console.WriteLine($"Дедлайн: {deadline}");
                    Console.WriteLine("Участники:");
                    currentProject = projectName;
                }

                Console.WriteLine($"  - {nickname} ({role})");
                Console.WriteLine($"    Паспорт: {passportData}");
                Console.WriteLine($"    Адрес: {address}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        DatabaseManager.InitializeDatabase();

        DatabaseManager.DisplayProjectParticipants();
    }
}