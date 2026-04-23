using ArtStudioApp;

const string connectionString = @"Data Source=C:\Users\COLLEGE\RiderProjects\ArtStudioApp\ArtStudioApp\studio.db;";

var dbContext = new DbContext(connectionString);

if (!dbContext.HasData())
{
    // Люди
    var person1 = new PersonDto(null, "SolarFlare", new DateOnly(2023, 1, 15));
    var person2 = new PersonDto(null, "MoonWhisper", new DateOnly(2023, 3, 22));
    var person3 = new PersonDto(null, "PixelCrafter", new DateOnly(2023, 6, 10));

    long id1 = dbContext.InsertPerson(person1);
    long id2 = dbContext.InsertPerson(person2);
    long id3 = dbContext.InsertPerson(person3);

    person1 = person1 with { Id = id1 };
    person2 = person2 with { Id = id2 };
    person3 = person3 with { Id = id3 };

    // Личные дела
    var personalInfo1 = new PersonalInfoDto(null, person1.Id!.Value,
        "Иванов", "Алексей", "Петрович",
        "4501", "123456", "ОВД района Творческий г. Москвы",
        new DateOnly(2018, 5, 10), "770-001", "г. Москва, ул. Творческая, д.10, кв.5");
    var personalInfo2 = new PersonalInfoDto(null, person2.Id!.Value,
        "Смирнова", "Елена", "Владимировна",
        "6702", "654321", "УФМС России по Санкт-Петербургу и Ленинградской области",
        new DateOnly(2019, 9, 15), "780-002", "г. Санкт-Петербург, пр. Невский, д.25, кв.12");
    var personalInfo3 = new PersonalInfoDto(null, person3.Id!.Value,
        "Каримов", "Рустам", "Маратович",
        "7803", "987654", "Отдел УФМС России по Республике Татарстан в г. Казани",
        new DateOnly(2020, 2, 20), "160-003", "г. Казань, ул. Баумана, д.5, кв.8");

    dbContext.InsertPersonalInfo(personalInfo1);
    dbContext.InsertPersonalInfo(personalInfo2);
    dbContext.InsertPersonalInfo(personalInfo3);

    // Проекты
    var project1 = new ProjectDto(null, "Короткометражный фильм 'Лунный свет'", new DateOnly(2024, 12, 1));
    var project2 = new ProjectDto(null, "Анимационный ролик 'Цифровая мечта'", new DateOnly(2024, 11, 15));

    long proj1Id = dbContext.InsertProject(project1);
    long proj2Id = dbContext.InsertProject(project2);

    project1 = project1 with { Id = proj1Id };
    project2 = project2 with { Id = proj2Id };

    // Назначения
    var assignments = new[]
    {
        new ProjectAssignmentDto(person1.Id!.Value, project1.Id!.Value, "Режиссёр"),
        new ProjectAssignmentDto(person2.Id!.Value, project1.Id!.Value, "Сценарист"),
        new ProjectAssignmentDto(person3.Id!.Value, project1.Id!.Value, "Монтажёр"),
        new ProjectAssignmentDto(person2.Id!.Value, project2.Id!.Value, "Ведущий аниматор"),
        new ProjectAssignmentDto(person3.Id!.Value, project2.Id!.Value, "Художник по текстурам")
    };

    foreach (var assignment in assignments)
    {
        dbContext.InsertProjectAssignment(assignment);
    }
}
else
{
    Console.WriteLine("Тестовые данные уже присутствуют в базе.");
}
Console.WriteLine("Отчет:");
var participants = dbContext.GetProjectParticipants().ToList();
if (participants.Count == 0)
{
    Console.WriteLine("Нет данных о проектах и участниках.");
    return;
}

string currentProject = "";
foreach (var p in participants)
{
    if (currentProject != p.ProjectTitle)
    {
        currentProject = p.ProjectTitle;
        Console.WriteLine($"Проект: {p.ProjectTitle}");
        Console.WriteLine($"Дедлайн: {p.DeadlineDate:dd.MM.yyyy}");
        Console.WriteLine("Участники:");
    }

    Console.WriteLine($"  - {p.Nickname} (роль: {p.Role})");
    Console.WriteLine($"    ФИО: {p.FullName}");
    Console.WriteLine($"    Паспорт: серия {p.PassportSeries}, номер {p.PassportNumber}");
    Console.WriteLine($"    Выдан: {p.PassportIssuedBy}, {p.PassportIssueDate:dd.MM.yyyy}");
    Console.WriteLine($"    Код подразделения: {p.DepartmentCode}");
    Console.WriteLine($"    Адрес: {p.Address}");
    Console.WriteLine();
}