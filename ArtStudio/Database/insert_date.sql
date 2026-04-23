INSERT INTO table_studios_participants (username, registration_date) 
VALUES ('JohnDoe', '2026-04-23');

INSERT INTO table_passports (
    participant_id, series, number, surname, first_name, patronymic, 
    gender, birth_date, birth_place, issue_date, issued_by, 
    department_code, registration_address
) VALUES (
    1, '4510', '123456', 'Иванов', 'Иван', 'Иванович', 
    'М', '1990-05-15', 'г. Москва', '2010-06-20', 'ТП №1', 
    '770-001', 'г. Москва'
);

INSERT INTO table_projects (title, start_date, deadline) 
VALUES ('Съемка рекламы', '2026-05-01', '2026-05-15');

INSERT INTO table_project_assignments (project_id, participant_id, role) 
VALUES (1, 1, 'Сценарист');



