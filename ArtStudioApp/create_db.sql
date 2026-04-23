CREATE TABLE table_performers
(
    id                INTEGER PRIMARY KEY AUTOINCREMENT,
    nickname          TEXT NOT NULL UNIQUE,
    registration_date TEXT NOT NULL
);

CREATE TABLE table_passport
(
    id               INTEGER PRIMARY KEY AUTOINCREMENT,
    series           INT  NOT NULL,
    number           INT  NOT NULL,
    issued_by        TEXT NOT NULL,
    issued_date      TEXT NOT NULL,
    departament_code INT  NOT NULL,
    UNIQUE (series, number)
);

CREATE TABLE table_personal_files
(
    id           INTEGER PRIMARY KEY AUTOINCREMENT,
    performer_id INTEGER NOT NULL UNIQUE,
    passport_id  INTEGER NOT NULL UNIQUE,
    first_name   TEXT    NOT NULL,
    last_name    TEXT    NOT NULL,
    second_name  TEXT    NULL,
    address      TEXT    NOT NULL,
    FOREIGN KEY (performer_id) REFERENCES table_performers (id),
    FOREIGN KEY (passport_id) REFERENCES table_passport (id)
);

CREATE TABLE table_projects
(
    id       INTEGER PRIMARY KEY AUTOINCREMENT,
    name     TEXT NOT NULL,
    deadline TEXT NOT NULL
);

CREATE TABLE project_participations
(
    id           INTEGER PRIMARY KEY AUTOINCREMENT,
    performer_id INTEGER NOT NULL,
    project_id   INTEGER NOT NULL,
    role         TEXT    NOT NULL,
    FOREIGN KEY (performer_id) REFERENCES table_performers (id),
    FOREIGN KEY (project_id) REFERENCES table_projects (id),
    UNIQUE (performer_id, project_id)
);

CREATE VIEW view_projects_participants AS
SELECT table_projects.id                AS 'project_id',
       table_projects.name              AS 'project_name',
       table_projects.deadline          AS 'project_deadline',
       table_performers.id              AS 'performer_id',
       table_performers.nickname        AS 'performer_nickname',
       project_participations.role      AS 'performer_role',
       table_personal_files.first_name  AS 'first_name',
       table_personal_files.last_name   AS 'last_name',
       table_personal_files.second_name AS 'second_name',
       table_personal_files.address     AS 'address',
       table_passport.series            AS 'passport_series',
       table_passport.number            AS 'passport_number',
       table_passport.issued_by         AS 'passport_issued_by',
       table_passport.issued_date       AS 'passport_issued_date',
       table_passport.departament_code  AS 'passport_department_code'
FROM table_projects
         JOIN project_participations ON table_projects.id = project_participations.project_id
         JOIN table_performers ON project_participations.performer_id = table_performers.id
         JOIN table_personal_files ON table_performers.id = table_personal_files.performer_id
         JOIN table_passport ON table_personal_files.passport_id = table_passport.id;

INSERT INTO table_passport (series, number, issued_by, issued_date, departament_code)
VALUES
    (4512, 345678, 'ОУФМС России по г. Москве', '2015-06-15', 770001),
    (4513, 987654, 'ГУ МВД России по г. Санкт-Петербургу', '2016-03-20', 780002),
    (4514, 567890, 'Отдел МВД России по г. Екатеринбургу', '2017-11-10', 660003);

INSERT INTO table_performers (nickname, registration_date)
VALUES
    ('CreativeMaster', '2025-10-15 10:30:00'),
    ('VideoWizard', '2025-12-20 14:15:00'),
    ('ScriptGuru', '2026-01-10 09:45:00');

INSERT INTO table_personal_files (performer_id, passport_id, first_name, last_name, second_name, address)
VALUES
    (1, 1, 'Иван', 'Иванов', 'Петрович', 'г. Москва, ул. Тверская, д. 15, кв. 45'),
    (2, 2, 'Анна', 'Петрова', 'Сергеевна', 'г. Санкт-Петербург, Невский пр., д. 25, кв. 12'),
    (3, 3, 'Алексей', 'Сидоров', 'Дмитриевич', 'г. Екатеринбург, ул. Ленина, д. 50, кв. 8');

INSERT INTO table_projects (name, deadline)
VALUES
    ('Летнее видео-шоу', '2026-06-23'),
    ('Осенний музыкальный фестиваль', '2026-09-23');

INSERT INTO project_participations (performer_id, project_id, role)
VALUES
    (1, 1, 'Ведущий'),      
    (2, 1, 'Монтажёр'),     
    (3, 1, 'Сценарист');

INSERT INTO project_participations (performer_id, project_id, role)
VALUES
    (2, 2, 'Ведущий'),      
    (3, 2, 'Сценарист');