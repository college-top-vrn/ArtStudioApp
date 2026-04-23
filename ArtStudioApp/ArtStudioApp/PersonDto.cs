namespace ArtStudioApp;

public record PersonDto(
    long? Id,
    string Nickname,
    DateOnly RegistrationDate
    );