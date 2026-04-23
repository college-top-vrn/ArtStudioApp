CREATE VIEW view_projects_with_full_team AS
SELECT
    proj.id AS project_id,
    proj.name AS project_name,
    proj.deadline,
    pi.nickname,
    pa.roles,
    pd.last_name,
    pd.first_name,
    pd.patronymic,
    pd.series,
    pd.number,   
    pd.issued_by,
    pd.date_of_issue,
    pd.department_code,
    pd.residential_address
FROM table_projects proj
         LEFT JOIN table_project_activity pa ON proj.id = pa.project_id
         LEFT JOIN table_personal_informations pi ON pa.executor_id = pi.id
         LEFT JOIN table_personal_data pd ON pi.id = pd.personal_id;