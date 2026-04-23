using System;
using System.Collections.Generic;
using System.Data;
using DefaultNamespace;
using Microsoft.Data.Sqlite;

const string CONNECTION_STRING = @"Data Source=C:\Users\college\RiderProjects\ArtStudioApp1\creativeStudio.db;";
var teams = new List<ProjectTeamMember>();
using (var db = new SqliteConnection(CONNECTION_STRING))
{
    db.Open();
    
    const string sql = """
                       SELECT * FROM view_projects_with_full_team 
                                ORDER BY project_name, nickname
                       """;
    using var command = new SqliteCommand(sql , db);
    using var reader = command.ExecuteReader();
    
    while (reader.Read())
    {
        teams.Add(new ProjectTeamMember
        {
            ProjectId = reader.GetInt32("project_id"),
            ProjectName = reader.GetString("project_name"),
            Deadline =  DateOnly.Parse(reader.GetString("deadline")),
            Nickname = reader.IsDBNull("nickname") ? null : reader.GetString("nickname"),
            Roles = reader.IsDBNull("roles") ? null : reader.GetString("roles"),
            LastName = reader.IsDBNull("last_name") ? null : reader.GetString("last_name"),
            FirstName = reader.IsDBNull("first_name") ? null : reader.GetString("first_name"),
            Patronymic = reader.IsDBNull("patronymic") ? null : reader.GetString("patronymic"),
            Series = reader.IsDBNull("series") ? null : reader.GetString("series"),
            Number = reader.IsDBNull("number") ? null : reader.GetString("number"),
            IssuedBy = reader.IsDBNull("issued_by") ? null : reader.GetString("issued_by"),
            DateOfIssue =reader.IsDBNull("date_of_issue") ? null : DateOnly.Parse(reader.GetString("date_of_issue")),
            DepartmentCode = reader.IsDBNull("department_code") ? null : reader.GetString("department_code"),
            ResidentialAddress = reader.IsDBNull("residential_address") ? null : reader.GetString("residential_address")
        });
    }
}

foreach (var team in teams)
{
    Console.WriteLine(team);
}