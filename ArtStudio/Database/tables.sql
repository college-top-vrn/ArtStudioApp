CREATE TABLE IF NOT EXISTS table_passports
(
    id                   INTEGER PRIMARY KEY AUTOINCREMENT,
    participant_id       INTEGER NOT NULL UNIQUE,
    series               TEXT    NOT NULL CHECK ( length(series) = 4 ),
    number               TEXT    NOT NULL CHECK ( length(number) = 6 ),
    surname              TEXT    NOT NULL,
    first_name           TEXT    NOT NULL,
    patronymic           TEXT,
    gender               TEXT CHECK (gender IN ('М', 'Ж')),
    birth_date           TEXT    NOT NULL,
    birth_place          TEXT    NOT NULL,
    issue_date           TEXT    NOT NULL CHECK (issue_date >= birth_date),
    issued_by            TEXT    NOT NULL,
    department_code      TEXT    NOT NULL,
    registration_address TEXT,

    FOREIGN KEY (participant_id) REFERENCES table_studios_participants (id) ON DELETE RESTRICT,
    UNIQUE (series, number)
);



CREATE TABLE IF NOT EXISTS table_studios_participants
(
    id                INTEGER PRIMARY KEY AUTOINCREMENT,
    username          TEXT NOT NULL UNIQUE CHECK ( length(username) >= 3 ),
    registration_date TEXT NOT NULL
);


CREATE TABLE IF NOT EXISTS table_projects
(
    id         INTEGER PRIMARY KEY AUTOINCREMENT,
    title      TEXT NOT NULL UNIQUE CHECK ( length(title) > 0 ),
    start_date TEXT NOT NULL,
    deadline   TEXT NOT NULL CHECK (deadline >= start_date)
);



CREATE TABLE IF NOT EXISTS table_project_assignments
(
    project_id     INTEGER NOT NULL,
    participant_id INTEGER NOT NULL,
    role           TEXT    NOT NULL CHECK (length(role) > 0 ),

    PRIMARY KEY (project_id, participant_id),
    FOREIGN KEY (project_id) REFERENCES table_projects (id) ON DELETE RESTRICT,
    FOREIGN KEY (participant_id) REFERENCES table_studios_participants (id) ON DELETE RESTRICT
);



CREATE VIEW IF NOT EXISTS view_project_details AS
SELECT 
    pr.title AS ProjectTitle,
    pr.deadline AS ProjectDeadline,
    part.username AS ParticipantUserName,
    assign.role AS ProjectRole,
    pass.surname AS Surname,
    pass.first_name AS FirstName
FROM table_projects pr
JOIN table_project_assignments assign ON pr.id = assign.project_id
JOIN table_studios_participants part ON assign.participant_id = part.id
JOIN table_passports pass ON part.id = pass.participant_id;