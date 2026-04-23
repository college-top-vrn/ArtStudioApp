namespace ArtStudioApp;

public record class PersonalData
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public string? SecondName { get; init; }
    public required string Address { get; init; }
    public required PassportInfo Passport { get; init; }
    
    public string FullName => SecondName == null 
        ? $"{LastName} {FirstName}" 
        : $"{LastName} {FirstName} {SecondName}";
}