classDiagram
direction BT

class table_personal_data {
   text number
   text series
   text issued_by
   text date_of_issue
   text department_code
   text last_name
   text first_name
   text patronymic
   text residential_address
   integer personal_id
   integer id
}
class table_personal_informations {
   text nickname
   text date_of_registration
   integer id
}
class table_project_activity {
   integer executor_id
   integer project_id
   text roles
   integer id
}
class table_projects {
   text name
   text deadline
   integer id
}
class view_projects_with_full_team {
   integer project_id
   text project_name
   text deadline
   text nickname
   text roles
   text last_name
   text first_name
   text patronymic
   text series
   text number
   text issued_by
   text date_of_issue
   text department_code
   text residential_address
}

table_personal_data  -->  table_personal_informations : personal_id:id
table_project_activity  -->  table_personal_informations : executor_id:id
table_project_activity  -->  table_projects : project_id:id
