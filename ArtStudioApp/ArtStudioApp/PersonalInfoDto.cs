namespace ArtStudioApp;

public record PersonalInfoDto(
    long? Id,
    long PersonId,
    string LastName,
    string FirstName,
    string? Patronymic,
    string PassportSeries,
    string PassportNumber,
    string PassportIssuedBy,
    DateOnly PassportIssueDate,
    string PassportDepartmentCode,
    string Address
    );