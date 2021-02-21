SELECT employee.department_id, department.name FROM employee
 JOIN department ON department.id = employee.department_id
 WHERE salary in (SELECT MAX(salary) FROM employee);