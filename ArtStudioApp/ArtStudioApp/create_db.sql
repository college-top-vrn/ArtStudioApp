CREATE TABLE IF NOT EXISTS table_persons(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    nickname TEXT NOT NULL,
    registration_date TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS table_personal_info(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    person_id INTEGER NOT NULL UNIQUE,
    last_name TEXT NOT NULL,
    first_name TEXT NOT NULL,
    patronymic TEXT,
    passport_series TEXT NOT NULL CHECK ( length(passport_series) = 4 ),
    passport_number TEXT NOT NULL CHECK ( length(passport_number) = 6 ),
    passport_issued_by TEXT NOT NULL,
    passport_issue_date TEXT NOT NULL,
    passport_department_code TEXT NOT NULL,
    address TEXT NOT NULL,
    FOREIGN KEY (person_id) REFERENCES table_persons(id) ON DELETE CASCADE
);

CREATE TABLE IF NOT EXISTS table_projects(
    id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    title TEXT NOT NULL,
    deadline_date TEXT NOT NULL
);

CREATE TABLE IF NOT EXISTS table_project_assignments(
    person_id INTEGER NOT NULL,
    project_id INTEGER NOT NULL,
    person_role TEXT NOT NULL,
    PRIMARY KEY (person_id, project_id),
    FOREIGN KEY (person_id) REFERENCES table_persons(id) ON DELETE CASCADE,
    FOREIGN KEY (project_id) REFERENCES table_projects(id) ON DELETE CASCADE
);