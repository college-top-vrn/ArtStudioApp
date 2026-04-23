--region Структура БД
CREATE TABLE table_personal_files
(
    id                  INTEGER PRIMARY KEY AUTOINCREMENT,
    name                TEXT    NOT NULL CHECK ( length(name) > 0 ),
    surname             TEXT    NOT NULL CHECK ( length(surname) > 0 ),
    patronymic          TEXT    NOT NULL DEFAULT '',
    gender              INTEGER NOT NULL CHECK ( gender == 0 OR gender == 1 ),
    series              TEXT    NOT NULL CHECK ( length(series) > 0 ),
    number              TEXT    NOT NULL CHECK ( length(number) > 0 ),
    birth_date          TEXT    NOT NULL CHECK ( length(birth_date) > 0 ),
    birth_place         TEXT    NOT NULL CHECK ( length(birth_place) > 0 ),
    residential_address TEXT    NOT NULL CHECK ( length(residential_address) > 0 )
);

CREATE TABLE table_roles
(
    id   INTEGER PRIMARY KEY AUTOINCREMENT,
    role TEXT NOT NULL CHECK ( length(role) > 0 )
);

CREATE TABLE table_project_activities
(
    id            INTEGER PRIMARY KEY AUTOINCREMENT,
    name          TEXT NOT NULL CHECK ( length(name) > 0 ),
    deadline_date TEXT NOT NULL CHECK ( length(deadline_date) > 0 )
);

CREATE TABLE table_executors
(
    id                INTEGER PRIMARY KEY AUTOINCREMENT,
    alias             TEXT NOT NULL CHECK ( length(alias) > 0 ),
    registration_date TEXT NOT NULL CHECK ( length(registration_date) > 0 ),
    personal_file_id  INTEGER REFERENCES table_personal_files (id)
);

CREATE TABLE table_project_roles
(
    id               INTEGER PRIMARY KEY AUTOINCREMENT,
    project_id       INTEGER REFERENCES table_project_activities (id),
    executor_role_id INTEGER REFERENCES table_roles (id),
    executor_id      INTEGER REFERENCES table_executors (id)
);
--endregion

--region Данные
INSERT INTO table_personal_files (name, surname, patronymic, gender, series, number, birth_date, birth_place,
                                  residential_address)
VALUES ('Александр', 'Иванов', 'Сергеевич', 0, '4510', '123456', '1990-05-15', 'г. Москва',
        'г. Москва, ул. Ленина, д. 10, кв. 5'),
       ('Мария', 'Петрова', 'Алексеевна', 1, '4512', '654321', '1995-08-22', 'г. Новосибирск',
        'г. Новосибирск, пр. Красный, д. 25, кв. 12'),
       ('Дмитрий', 'Сидоров', 'Николаевич', 0, '4615', '789012', '1988-03-10', 'г. Екатеринбург',
        'г. Екатеринбург, ул. Мира, д. 5, кв. 48'),
       ('Елена', 'Кузнецова', 'Игоревна', 1, '4514', '345678', '1992-11-30', 'г. Казань',
        'г. Казань, ул. Баумана, д. 12, кв. 3'),
       ('Алексей', 'Попов', 'Владимирович', 0, '5011', '901234', '1985-07-04', 'г. Самара',
        'г. Самара, ул. Садовая, д. 102, кв. 15'),
       ('Анна', 'Смирнова', 'Васильевна', 1, '4508', '567890', '1998-01-14', 'г. Воронеж',
        'г. Воронеж, ул. Кольцовская, д. 33, кв. 89'),
       ('Игорь', 'Волков', 'Артемович', 0, '6012', '234567', '1993-09-27', 'г. Красноярск',
        'г. Красноярск, ул. Ленина, д. 55, кв. 10'),
       ('Ольга', 'Морозова', '', 1, '4519', '890123', '1991-04-05', 'г. Тюмень',
        'г. Тюмень, ул. Республики, д. 14, кв. 22'),
       ('Максим', 'Павлов', 'Денисович', 0, '4516', '456789', '1987-12-12', 'г. Омск',
        'г. Омск, ул. Гагарина, д. 8, кв. 54'),
       ('Светлана', 'Козлова', 'Михайловна', 1, '4505', '123987', '1996-06-18', 'г. Уфа',
        'г. Уфа, ул. Ленина, д. 2, кв. 41');

INSERT INTO table_roles (role)
VALUES ('Руководитель студии'),
       ('Администратор'),
       ('Преподаватель живописи'),
       ('Преподаватель графики'),
       ('Мастер по керамике'),
       ('Педагог по вокалу'),
       ('Хореограф'),
       ('Фотограф'),
       ('SMM-менеджер'),
       ('Уборщик');

INSERT INTO table_project_activities (name, deadline_date)
VALUES ('ArtGallery-SPA: Виртуальная выставка на React', '2024-05-20'),
       ('Watercolor-Tutorials: Обучающая платформа', '2024-04-15'),
       ('VocalStream: Сервис для стриминга концертов', '2024-06-01'),
       ('CeramicBid: Микросервис для онлайн-аукциона', '2024-07-10'),
       ('GeoPainter: Мобильное приложение для поиска локаций', '2024-05-05'),
       ('DigiSketch-Tool: Инструмент для работы с вектором', '2024-04-28'),
       ('DanceSync: Трекер движений на OpenCV', '2024-06-15'),
       ('StudioShot-API: Бэкенд для фотохостинга', '2024-04-20'),
       ('AcademicDraw-Bot: Телеграм-бот с уроками рисунка', '2024-08-30'),
       ('OpenDoor-Manager: Система регистрации посетителей', '2024-09-01');


INSERT INTO table_executors (alias, registration_date, personal_file_id)
VALUES ('SunnyBrush', '2023-01-10', 1),
       ('MoonLight_Art', '2023-02-15', 2),
       ('ClayMaster', '2023-03-20', 3),
       ('VocalStar', '2023-05-12', 4),
       ('DeepGraphite', '2023-06-01', 5),
       ('UfaCreative', '2023-07-22', 10),
       ('SiberianArtist', '2023-08-05', 7),
       ('Moro_Design', '2023-09-14', 8),
       ('PixelHunter', '2023-10-30', 9),
       ('Aero_Visual', '2023-11-11', 6);

INSERT INTO table_project_roles (project_id, executor_role_id, executor_id)
VALUES (1, 1, 1),
       (1, 9, 6),
       (2, 3, 2),
       (3, 6, 4),
       (4, 5, 3),
       (5, 3, 7),
       (6, 4, 5),
       (8, 8, 9),
       (9, 4, 2),
       (10, 2, 10);
--endregion