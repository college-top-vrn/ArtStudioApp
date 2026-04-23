namespace ArtStudioAppBS.Entities;

public record Executor
{
    public required int Id { get; init; }
    public required string Alias { get; init; }
    public required string RegistrationDate { get; init; }
    public required int PersonalFileId { get; init; }
}