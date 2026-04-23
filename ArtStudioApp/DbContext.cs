using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace ArtStudioApp;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

public List<ProjectInfo> GetAllProjectsInfo()
{
    var projectsDict = new Dictionary<string, ProjectInfo>();

    using var db = new SqliteConnection(_connectionString);
    db.Open();

    const string sql = """
                       SELECT 
                           project_name,
                           project_deadline,
                           performer_nickname,
                           performer_role,
                           passport_series,
                           passport_number,
                           passport_issued_by,
                           passport_issued_date,
                           passport_department_code,
                           first_name,
                           last_name,
                           second_name,
                           address
                       FROM view_projects_participants
                       ORDER BY project_id, performer_id
                       """;

    using var cmd = db.CreateCommand();
    cmd.CommandText = sql;

    using var reader = cmd.ExecuteReader();

    while (reader.Read())
    {
        string projectName = reader.GetString("project_name");

        if (!projectsDict.ContainsKey(projectName))
        {
            projectsDict[projectName] = new ProjectInfo
            {
                Name = projectName,
                Deadline = DateTime.Parse(reader.GetString("project_deadline")),
                Participants = new List<ParticipantInfo>()
            };
        }

        if (!reader.IsDBNull("performer_nickname"))
        {
            var passport = new PassportInfo
            {
                Series = reader.GetInt32("passport_series"),
                Number = reader.GetInt32("passport_number"),
                IssuedBy = reader.GetString("passport_issued_by"),
                IssuedDate = DateTime.Parse(reader.GetString("passport_issued_date")),
                DepartmentCode = reader.GetInt32("passport_department_code")
            };

            var personalData = new PersonalData
            {
                FirstName = reader.GetString("first_name"),
                LastName = reader.GetString("last_name"),
                SecondName = reader.IsDBNull("second_name") ? null : reader.GetString("second_name"),
                Address = reader.GetString("address"),
                Passport = passport
            };

            var participant = new ParticipantInfo
            {
                Nickname = reader.GetString("performer_nickname"),
                Role = reader.GetString("performer_role"),
                PersonalInfo = personalData
            };

            projectsDict[projectName].Participants.Add(participant);
        }
    }

    return projectsDict.Values.ToList();
}

public void DisplayAllProjectsInfo()
{
    var projects = GetAllProjectsInfo();
    
    Console.WriteLine("\n=== ИНФОРМАЦИЯ О ПРОЕКТАХ ===\n");
    
    foreach (var project in projects)
    {
        Console.WriteLine($" ПРОЕКТ: {project.Name}");
        Console.WriteLine($"Дедлайн: {project.Deadline:dd.MM.yyyy}");
        Console.WriteLine($"Участников: {project.Participants.Count}\n");
        
        foreach (var participant in project.Participants)
        {
            Console.WriteLine($"  • Псевдоним: {participant.Nickname}");
            Console.WriteLine($"    Роль: {participant.Role}");
            Console.WriteLine($"    ФИО: {participant.PersonalInfo.FullName}");
            Console.WriteLine($"    Паспорт: {participant.PersonalInfo.Passport.AllInfo}");
            Console.WriteLine($"    Адрес: {participant.PersonalInfo.Address}");
            Console.WriteLine();
        }
        
        Console.WriteLine(new string('=', 80));
    }
}

}