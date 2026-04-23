PRAGMA foreign_keys = OFF;

DROP TABLE IF EXISTS ProjectParticipants;
DROP TABLE IF EXISTS PersonalFiles;
DROP TABLE IF EXISTS Projects;
DROP TABLE IF EXISTS Performers;

PRAGMA foreign_keys = ON;

CREATE TABLE Performers
(
    Id               INTEGER PRIMARY KEY AUTOINCREMENT,
    Nickname         TEXT NOT NULL,
    RegistrationDate TEXT NOT NULL
);

CREATE TABLE PersonalFiles
(
    Id           INTEGER PRIMARY KEY AUTOINCREMENT,
    PerformerId  INTEGER UNIQUE NOT NULL,
    PassportData TEXT           NOT NULL,
    Address      TEXT           NOT NULL,
    FOREIGN KEY (PerformerId) REFERENCES Performers (Id)
);

CREATE TABLE Projects
(
    Id       INTEGER PRIMARY KEY AUTOINCREMENT,
    Name     TEXT NOT NULL,
    Deadline TEXT NOT NULL
);

CREATE TABLE ProjectParticipants
(
    ProjectId   INTEGER NOT NULL,
    PerformerId INTEGER NOT NULL,
    Role        TEXT    NOT NULL,
    PRIMARY KEY (ProjectId, PerformerId),
    FOREIGN KEY (ProjectId) REFERENCES Projects (Id),
    FOREIGN KEY (PerformerId) REFERENCES Performers (Id)
);

INSERT INTO Performers (Nickname, RegistrationDate)
VALUES ('Александор Викторов', '2023-01-15'),
       ('Владлена Миризе', '2023-02-20'),
       ('Мария Щукина', '2023-03-10');

INSERT INTO PersonalFiles (PerformerId, PassportData, Address)
VALUES (1, '5643 123745', 'г. Москва, ул. Тверская 5'),
       (2, '8723 845827', 'Санкт-Петербург, Невский 15'),
       (3, '3949 957838', 'Казань, ул. Баумана 10');

INSERT INTO Projects (Name, Deadline)
VALUES ('Metro "2039"', '2023-12-31'),
       ('Arknight', '2023-11-15');
INSERT INTO ProjectParticipants (ProjectId, PerformerId, Role)
VALUES (1, 1, 'Озвучики');
INSERT INTO ProjectParticipants (ProjectId, PerformerId, Role)
VALUES (1, 2, 'Монтажёр');
INSERT INTO ProjectParticipants (ProjectId, PerformerId, Role)
VALUES (1, 3, 'Сценарист');
INSERT INTO ProjectParticipants (ProjectId, PerformerId, Role)
VALUES (2, 1, 'Дизайнер');
INSERT INTO ProjectParticipants (ProjectId, PerformerId, Role)
VALUES (2, 2, 'Разработчик');