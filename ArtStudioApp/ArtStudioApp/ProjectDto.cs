namespace ArtStudioApp;

public record ProjectDto(
    long? Id,
    string Title,
    DateOnly DeadlineDate
    );