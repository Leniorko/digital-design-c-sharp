-- In my case it is not possible to get id with name because names are repetative.
SELECT department.name, SUM(salary) as sallary_sum FROM employee
 JOIN department ON department.id = employee.department_id
 GROUP BY department.name
 ORDER BY sallary_sum DESC LIMIT 1;