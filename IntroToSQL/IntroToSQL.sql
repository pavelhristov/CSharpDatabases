--1. SQL = Structured Query Language. Declarative language for query and manipulation of relational data. DML = Data Manipulation Language. DDL = Data Definition Language. Commands SELECT, INSERT, UPDATE, DELETE, CREATE, DROP, ALTER, GRANT, REVOKE.

--2. T-SQL is an extension to the standard SQL language. Standard language used in MS SQL Server.

--3. Start SQL Management Studio and connect to the database TelerikAcademy. Examine the major tables in the "TelerikAcademy" database.
--Script from GitHub repository
USE TelerikAcademy

--4. Write a SQL query to find all information about all departments (use "TelerikAcademy" database).
SELECT *
FROM Departments

--5. Write a SQL query to find all department names.
SELECT Name 
FROM Departments

--6. Write a SQL query to find the salary of each employee.
SELECT FirstName, LastName, Salary
FROM Employees

--7. Write a SQL to find the full name of each employee.
SELECT FirstName + ' ' +  LastName AS [FullName]
FROM Employees

--8. Write a SQL query to find the email addresses of each employee (by his first and last name). Consider that the mail domain is telerik.com. Emails should look like “John.Doe@telerik.com". The produced column should be named "Full Email Addresses".
SELECT FirstName + '.' +  LastName + '@telerik.com' AS [Full Email Addresses]
FROM Employees

--9. Write a SQL query to find all different employee salaries.
SELECT Salary 
FROM Employees
GROUP BY Salary

--10. Write a SQL query to find all information about the employees whose job title is “Sales Representative“.
SELECT *
FROM Employees
WHERE JobTitle = 'Sales Representative'

--11. Write a SQL query to find the names of all employees whose first name starts with "SA".
SELECT FirstName, LastName
FROM Employees
WHERE FirstName LIKE 'SA%'

--12. Write a SQL query to find the names of all employees whose last name contains "ei".
SELECT FirstName, LastName
FROM Employees
WHERE LastName LIKE '%ei%'

--13. Write a SQL query to find the salary of all employees whose salary is in the range [20000…30000].
SELECT Salary
FROM Employees
WHERE Salary BETWEEN 20000 AND 30000
GROUP BY Salary

--14. Write a SQL query to find the names of all employees whose salary is 25000, 14000, 12500 or 23600.
SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary IN (25000, 14000, 12500, 23600)

--15. Write a SQL query to find all employees that do not have manager.
SELECT *
FROM Employees
WHERE ManagerID IS NULL

--16. Write a SQL query to find all employees that have salary more than 50000. Order them in decreasing order by salary.
SELECT *
FROM Employees
WHERE Salary>50000
ORDER BY Salary DESC

--17. Write a SQL query to find the top 5 best paid employees.
SELECT TOP 5 *
FROM Employees
ORDER BY Salary DESC

--18. Write a SQL query to find all employees along with their address. Use inner join with ON clause.
SELECT emp.FirstName, emp.LastName, addr.AddressText, t.Name AS [Town]
FROM Employees emp
INNER JOIN Addresses addr
ON emp.AddressID = addr.AddressID
INNER JOIN Towns t
ON addr.TownID = t.TownID

--19. Write a SQL query to find all employees and their address. Use equijoins (conditions in the WHERE clause).
SELECT emp.FirstName, emp.LastName, addr.AddressText, t.Name AS [Town]
FROM Employees emp, Addresses addr, Towns t
WHERE emp.AddressID = addr.AddressID
AND addr.TownID = t.TownID

--20. Write a SQL query to find all employees along with their manager.
SELECT emp.FirstName, emp.LastName, mng.LastName AS [Manager]
FROM Employees emp, Employees mng
WHERE emp.ManagerID = mng.EmployeeID

--21. Write a SQL query to find all employees, along with their manager and their address. Join the 3 tables: Employees e, Employees m and Addresses a.
SELECT e.FirstName, e.LastName, m.LastName AS [Manager], a.AddressText AS [Address]
FROM Employees e, Employees m, Addresses a
WHERE e.ManagerID = m.EmployeeID 
AND e.AddressID = a.AddressID

--22. Write a SQL query to find all departments and all town names as a single list. Use UNION.
SELECT d.Name
FROM Departments d
UNION
SELECT t.Name
FROM Towns t

--23. Write a SQL query to find all the employees and the manager for each of them along with the employees that do not have manager. Use right outer join.
SELECT e.FirstName, e.LastName, m.LastName AS [Manager]
FROM Employees m
RIGHT OUTER JOIN Employees e
ON m.EmployeeID = e.ManagerID
--Rewrite the query to use left outer join.
SELECT e.FirstName, e.LastName, m.LastName AS [Manager]
FROM Employees e
LEFT OUTER JOIN Employees m
ON m.EmployeeID = e.ManagerID

--24. Write a SQL query to find the names of all employees from the departments "Sales" and "Finance" whose hire year is between 1995 and 2005.
SELECT e.FirstName, e.LastName, e.HireDate, d.Name
FROM Employees e, Departments d
WHERE e.DepartmentID = d.DepartmentID 
AND d.Name IN ('Sales', 'Finance')
AND YEAR(e.HireDate) BETWEEN 1995 AND 2005
