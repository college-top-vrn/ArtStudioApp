using System.Data;
using Microsoft.Data.Sqlite;

namespace ArtStudioApp;

public class DbContext(string connectionString)
{
    private readonly SqliteConnection _db = new(connectionString);

    public bool HasData()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();
        var cmd = connection.CreateCommand();
        cmd.CommandText = "SELECT COUNT(*) FROM table_persons;";
        var count = Convert.ToInt64(cmd.ExecuteScalar());
        return count >= 3;
    }
    
    public long InsertPerson(PersonDto person)
    {
        _db.Open();
        var cmd = _db.CreateCommand();
        cmd.CommandText = """
                          INSERT INTO table_persons (nickname, registration_date)
                          VALUES (@nickname, @reg_date);
                          SELECT last_insert_rowid();
                          """;
        cmd.Parameters.AddWithValue("@nickname", person.Nickname);
        cmd.Parameters.AddWithValue("@reg_date", person.RegistrationDate.ToString("yyyy-MM-dd"));
        var result = Convert.ToInt64(cmd.ExecuteScalar());
        _db.Close();
        return result;
    }
    
    public long InsertPersonalInfo(PersonalInfoDto personalInfo)
        {
            _db.Open();
            var cmd = _db.CreateCommand();
            cmd.CommandText = """
                              INSERT INTO table_personal_info (person_id, last_name,
                                                               first_name, patronymic, 
                                                               passport_series, passport_number,
                                                               passport_issued_by, passport_issue_date,
                                                               passport_department_code, address)
                              VALUES 
                              (@person_id, @last_name, @first_name, @patronymic, @pas_series, @pas_number, @pas_issued_by, @pas_issue_date, @pas_dept_code, @address);
                              SELECT last_insert_rowid();
                              """;
            cmd.Parameters.AddWithValue("@person_id", personalInfo.PersonId);
            cmd.Parameters.AddWithValue("@last_name", personalInfo.LastName);
            cmd.Parameters.AddWithValue("@first_name", personalInfo.FirstName);
            cmd.Parameters.AddWithValue("@patronymic", personalInfo.Patronymic ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@pas_series", personalInfo.PassportSeries);
            cmd.Parameters.AddWithValue("@pas_number", personalInfo.PassportNumber);
            cmd.Parameters.AddWithValue("@pas_issued_by", personalInfo.PassportIssuedBy);
            cmd.Parameters.AddWithValue("@pas_issue_date", personalInfo.PassportIssueDate.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@pas_dept_code", personalInfo.PassportDepartmentCode);
            cmd.Parameters.AddWithValue("@address", personalInfo.Address);
            var result = Convert.ToInt64(cmd.ExecuteScalar());
            _db.Close();
            return result;
        }

    public long InsertProject(ProjectDto project)
    {
        _db.Open();
        var cmd = _db.CreateCommand();
        cmd.CommandText = """
                          INSERT INTO table_projects (title, deadline_date)
                          VALUES (@title, @deadline_date);
                          SELECT last_insert_rowid();
                          """;
        cmd.Parameters.AddWithValue("@title", project.Title);
        cmd.Parameters.AddWithValue("@deadline_date", project.DeadlineDate.ToString("yyyy-MM-dd"));
        var result = Convert.ToInt64(cmd.ExecuteScalar());
        _db.Close();
        return result;
    }

    public bool InsertProjectAssignment(ProjectAssignmentDto projectAssignment)
    {
        _db.Open();
        var cmd = _db.CreateCommand();
        cmd.CommandText = """
                          INSERT INTO table_project_assignments (person_id, project_id, person_role)
                          VALUES (@person_id, @project_id, @person_role);
                          """;
        cmd.Parameters.AddWithValue("@person_id", projectAssignment.PersonId);
        cmd.Parameters.AddWithValue("@project_id", projectAssignment.ProjectId);
        cmd.Parameters.AddWithValue("@person_role", projectAssignment.Role);
        var result = cmd.ExecuteNonQuery();
        _db.Close();
        return result > 0;
    }
    
    public IEnumerable<ProjectParticipantInfoDto> GetProjectParticipants()
    {
        var result = new List<ProjectParticipantInfoDto>();
        
        _db.Open();

        var cmd = _db.CreateCommand();
        cmd.CommandText = """
                          SELECT 
                              p.title,
                              p.deadline_date,
                              pers.nickname,
                              pa.person_role,
                              pi.last_name,
                              pi.first_name,
                              pi.patronymic,
                              pi.passport_series,
                              pi.passport_number,
                              pi.passport_issued_by,
                              pi.passport_issue_date,
                              pi.passport_department_code,
                              pi.address
                          FROM table_projects p
                          JOIN table_project_assignments pa ON p.Id = pa.project_id
                          JOIN table_persons pers ON pa.person_id = pers.id
                          JOIN table_personal_info pi ON pers.id = pi.person_id;
                          """;

        using var reader = cmd.ExecuteReader();
        if (!reader.HasRows) return result;
        while (reader.Read())
        {
            var info = new ProjectParticipantInfoDto(
                reader.GetString("title"),
                DateOnly.Parse(reader.GetString("deadline_date")),
                reader.GetString("nickname"),
                reader.GetString("person_role"),
                reader.GetString("last_name"),
                reader.GetString("first_name"),
                reader.IsDBNull("patronymic") ? null : reader.GetString("patronymic"),
                reader.GetString("passport_series"),
                reader.GetString("passport_number"),
                reader.GetString("passport_issued_by"),
                DateOnly.Parse(reader.GetString("passport_issue_date")),
                reader.GetString("passport_department_code"),
                reader.GetString("address")
            );
            result.Add(info);
        }

        return result;
    }
}