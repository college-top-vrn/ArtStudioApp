using ArtStudio.DTOs;

namespace ArtStudio.View;
using System;
using System.Collections.Generic;
using System.Linq;


public class ConsoleView
{
    public void DisplayProjectDetails(List<ProjectDetailDto> details)
    {
        Console.WriteLine("\n------ ОТЧЕТ ПО ПРОЕКТАМ СТУДИИ ------");
        var groupedByProject = details.GroupBy(d => new { d.ProjectTitle, d.Deadline });

        foreach (var project in groupedByProject)
        {
            Console.WriteLine($"\nПроект: {project.Key.ProjectTitle} (Дедлайн: {project.Key.Deadline})");
            Console.WriteLine(new string('-', 50));

            foreach (var p in project)
            {
                Console.WriteLine($"- Роль: {p.Role} | Ник: {p.Username}");
                Console.WriteLine($"  ФИО: {p.Surname} {p.FirstName}");
                Console.WriteLine($"  Паспорт РФ: {p.PassportSeries} {p.PassportNumber}\n");
            }
        }
    }
}