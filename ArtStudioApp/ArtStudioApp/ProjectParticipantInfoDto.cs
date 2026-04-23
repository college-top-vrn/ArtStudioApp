namespace ArtStudioApp;

public record ProjectParticipantInfoDto(
    string ProjectTitle,
    DateOnly DeadlineDate,
    string Nickname,
    string Role,
    string LastName,
    string FirstName,
    string? Patronymic,
    string PassportSeries,
    string PassportNumber,
    string PassportIssuedBy,
    DateOnly PassportIssueDate,
    string DepartmentCode,
    string Address
)
{
    public string FullName => $"{LastName} {FirstName}" + 
                              (string.IsNullOrEmpty(Patronymic) ? "" : $" {Patronymic}");
}