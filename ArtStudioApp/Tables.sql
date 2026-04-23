CREATE TABLE table_personal_informations
(
    id                   INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    nickname             TEXT    NOT NULL CHECK ( nickname != '' ),
    date_of_registration TEXT    NOT NULL CHECK (date_of_registration != '' )

);

CREATE TABLE table_personal_data
(
    id                  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    number              TEXT    NOT NULL,
    series              TEXT    NOT NULL,
    issued_by           TEXT    NOT NULL,
    date_of_issue       TEXT    NOT NULL,
    department_code     TEXT    NOT NULL,
    last_name           TEXT    NOT NULL CHECK (last_name != ''),
    first_name          TEXT    NOT NULL CHECK (first_name != ''),
    patronymic          TEXT    NULL,
    residential_address TEXT    NOT NULL,
    personal_id         INTEGER NOT NULL,
    FOREIGN KEY (personal_id) REFERENCES table_personal_informations (id)
);

CREATE TABLE table_projects
(
    id       INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    name     TEXT    NOT NULL CHECK (name != ''),
    deadline TEXT    NOT NULL CHECK ( deadline != '')
);

CREATE TABLE table_project_activity
(
    id          INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
    executor_id INTEGER NOT NULL,
    project_id  INTEGER NOT NULL,
    roles       TEXT    NOT NULL CHECK ( roles != ''),
    FOREIGN KEY (project_id) REFERENCES table_projects (id),
    FOREIGN KEY (executor_id) REFERENCES table_personal_informations (id)


);
    
        