using System;

namespace ArtStudioApp;

public record class PassportInfo
{
    public required int Series { get; init; }
    public required int Number { get; init; }
    public required string IssuedBy { get; init; }
    public required DateTime IssuedDate { get; init; }
    public required int DepartmentCode { get; init; }

    public string AllInfo =>
        $"{Series} {Number}, выдан: {IssuedBy}, дата: {IssuedDate:dd.MM.yyyy}, код: {DepartmentCode}";
    
}