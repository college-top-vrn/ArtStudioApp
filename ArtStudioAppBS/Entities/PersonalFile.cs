namespace ArtStudioAppBS.Entities;

public record PersonalFile
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required string Surname { get; init; }
    public required string Patronymic { get; init; }
    public required Gender Gender { get; init; }
    public required string Series { get; init; }
    public required string Number { get; init; }
    public required string BirthDate { get; init; }
    public required string BirthPlace { get; init; }
    public required string ResidentialAddress { get; init; }
}