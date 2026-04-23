namespace ArtStudio.DTOs;

public record ProjectDetailDto(
    string ProjectTitle,
    string Deadline,
    string Username,
    string Role,
    string Surname,
    string FirstName,
    string PassportSeries,
    string PassportNumber
);