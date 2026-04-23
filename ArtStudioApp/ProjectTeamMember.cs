using System;

namespace DefaultNamespace;

public record ProjectTeamMember(){
    public int ProjectId { get; init; }
    public string ProjectName{ get; init; }
    public DateOnly Deadline{ get; init; }
    public string? Nickname{ get; init; }
    public string? Roles{ get; init; }
    public string? LastName{ get; init; }
    public string? FirstName{ get; init; }
    public string? Patronymic{ get; init; }
    public string? Series{ get; init; }
    public string? Number{ get; init; }
    public string? IssuedBy{ get; init; }
    public DateOnly? DateOfIssue{ get; init; }
    public string? DepartmentCode{ get; init; }
    public string? ResidentialAddress{ get; init; }

    public override string ToString() =>
        $"#{ProjectId} {ProjectName} \t {Deadline} \t {Nickname} \t {Roles}";
};