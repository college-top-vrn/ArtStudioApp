using System;
using System.Collections.Generic;

namespace ArtStudioApp;

public class ProjectInfo
{
    public required string Name { get; init; }
    public required DateTime Deadline { get; init; }
    public List<ParticipantInfo> Participants { get; init; } = new List<ParticipantInfo>();
}