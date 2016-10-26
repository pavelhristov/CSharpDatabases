USE TelerikAcademy

--1. Write a SQL query to find the names and salaries of the employees that take the minimal salary in the company. Use a nested SELECT statement.
SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary = 
  (SELECT MIN(Salary) FROM Employees)

--2. Write a SQL query to find the names and salaries of the employees that have a salary that is up to 10% higher than the minimal salary for the company.
DECLARE @MinSalary int;
SELECT @MinSalary = MIN(Salary)
FROM Employees

SELECT FirstName, LastName, Salary
FROM Employees
WHERE Salary <= @MinSalary*110/100

--3. Write a SQL query to find the full name, salary and department of the employees that take the minimal salary in their department. Use a nested SELECT statement.
SELECT e.FirstName + ' ' + e.LastName AS [FullName] , d.Name AS [Departement], e.Salary
FROM Employees e, Departments d
WHERE Salary = 
  (SELECT MIN(Salary) FROM Employees 
   WHERE DepartmentID = e.DepartmentID)
AND e.DepartmentID = d.DepartmentID
ORDER BY e.DepartmentID

--4. Write a SQL query to find the average salary in the department #1.
SELECT AVG(Salary) AS [Avarage Salary] 
FROM Employees 
WHERE DepartmentID =1

--5. Write a SQL query to find the average salary in the "Sales" department.
SELECT AVG(e.Salary) AS [Avarage Salary] 
FROM Employees e, Departments d
WHERE e.DepartmentID = d.DepartmentID
AND d.Name = 'Sales'

--6. Write a SQL query to find the number of employees in the "Sales" department.
SELECT COUNT(e.FirstName) AS[Employees in Sales]
FROM Employees e, Departments d
WHERE e.DepartmentID = d.DepartmentID
AND d.Name = 'Sales'

--7. Write a SQL query to find the number of all employees that have manager.
SELECT COUNT(FirstName) AS[Employees with Manager]
FROM Employees 
WHERE ManagerID IS NOT NULL

--8. Write a SQL query to find the number of all employees that have no manager.
SELECT COUNT(FirstName) AS[Employees with Manager]
FROM Employees 
WHERE ManagerID IS NULL

--9. Write a SQL query to find all departments and the average salary for each of them.
SELECT d.Name, AVG(Salary) AS[Avarage Salary]
FROM Employees e, Departments d
WHERE e.DepartmentID = d.DepartmentID
GROUP BY d.Name

--10. Write a SQL query to find the count of all employees in each department and for each town.
SELECT t.Name AS [Town], d.Name AS [Departement], COUNT(e.FirstName) AS [Count]
FROM Employees e, Departments d, Towns t, Addresses a
WHERE e.DepartmentID = d.DepartmentID
AND e.AddressID = a.AddressID
AND a.TownID = t.TownID
GROUP BY d.Name, t.Name

--11. Write a SQL query to find all managers that have exactly 5 employees. Display their first name and last name.
--I'm aware it looks stupid, but it works! (•_•) ( •_•)>⌐■-■ (⌐■_■)
SELECT e.FirstName, e.LastName
FROM (SELECT m.FirstName, m.LastName, COUNT(e.FirstName) as [Count]
	FROM Employees e, Employees m
	WHERE e.ManagerID = m.EmployeeID
	GROUP BY m.LastName, m.FirstName) e
WHERE e.Count = 5

--12. Write a SQL query to find all employees along with their managers. For employees that do not have manager display the value "(no manager)".
SELECT *
FROM(SELECT e.FirstName, e.LastName, CASE WHEN e.ManagerID IS NULL THEN '(no manager)' ELSE m.FirstName +' '+m.LastName END AS [Manager]
FROM Employees e, Employees m
WHERE m.EmployeeID = e.ManagerID
OR e.ManagerID IS NULL
GROUP BY e.FirstName, e.LastName, m.FirstName, m.LastName, e.ManagerID) f
GROUP BY f.FirstName, f.LastName, f.Manager

--13. Write a SQL query to find the names of all employees whose last name is exactly 5 characters long. Use the built-in LEN(str) function.
SELECT FirstName, LastName
FROM Employees
WHERE LEN(LastName) = 5

--14. Write a SQL query to display the current date and time in the following format "day.month.year hour:minutes:seconds:milliseconds". 
SELECT CONVERT(NVARCHAR,GETDATE(),113)

--15. Write a SQL statement to create a table Users. Users should have username, password, full name and last login time. 
--Not sure why, you can find around the web that you are supposed to use DATALENGTH instead of LEN, but in this syntaxis it is not working correctly.
CREATE TABLE Users
(
	UserID int IDENTITY(1,1) NOT NULL,
	UserName nvarchar(50) NOT NULL UNIQUE,
	FullName nvarchar(100) NOT NULL,
	Password nvarchar(50) NOT NULL,
	CONSTRAINT USERS_PASSWORD_CK CHECK (LEN(Password) >= 5),
	LastLogin datetime,
	CONSTRAINT PK_Users PRIMARY KEY(UserID)
)
INSERT INTO Groups VALUES
('Pesho', 'Pesho Peshev', 'iiiiiiismaslo', GETDATE()),
('Gosho', 'Gosho Peshov', 'neegosho', GETDATE()),
('Trendafil', 'Trendafil Petrov', 'nqmaparola', GETDATE()),
('Ivan', 'Ivan Georgiev', 'ivanch000000', GETDATE()),
('Peshi', 'Petyr Draganov', '123456', GETDATE())



--16. Write a SQL statement to create a view that displays the users from the Users table that have been in the system today.
CREATE VIEW UsersView
AS
SELECT UserName, LastLogin
FROM Users
WHERE YEAR(LastLogin) = YEAR(GETDATE())
AND MONTH(LastLogin) = MONTH(GETDATE())
AND DAY(LastLogin) = DAY(GETDATE())

--17. Write a SQL statement to create a table Groups. Groups should have unique name (use unique constraint). Define primary key and identity column.
CREATE TABLE Groups
(
	GroupID int IDENTITY(1,1) NOT NULL,
	Name nvarchar(50) NOT NULL UNIQUE,
	CONSTRAINT PK_Groups PRIMARY KEY(GroupID)
)

--18. Write a SQL statement to add a column GroupID to the table Users. Fill some data in this new column and as well in the `Groups table. Write a SQL statement to add a foreign key constraint between tables Users and Groups tables.
ALTER TABLE Users
ADD GroupID int
CONSTRAINT FK_Users_Groups FOREIGN KEY(GroupID)
    REFERENCES Groups(GroupID)

INSERT INTO Groups VALUES
('Partial Differential Equations'),
('Physics'),
('Calculus'),
('Thermodynamic')

UPDATE Users
SET GroupID = 2
WHERE UserName='Pesho'; 

UPDATE Users
SET GroupID = 1
WHERE UserName='Gosho' 
OR UserName='Trendafil'; 

UPDATE Users
SET GroupID = 3
WHERE UserName='Ivan' 
OR UserName='Peshi'; 

--19. Write SQL statements to insert several records in the Users and Groups tables. ..............Already did it, way too lazy to copy-paste.
--20. Write SQL statements to update some of the records in the Users and Groups tables. LOOK UP^^
--21. Write SQL statements to delete some of the records from the Users and Groups tables.

DELETE Users
WHERE UserName='Peshi'; 


UPDATE Users
SET GroupID = NULL
WHERE GroupID = 1;
DELETE Groups
WHERE GroupID = 1;

--22. Write SQL statements to insert in the Users table the names of all employees from the Employees table. Combine the first and last names as a full name. For username use the first letter of the first name + the last name (in lowercase). Use the same for the password, and NULL for last login time.
--Andrew Hill & Annette Hill were hired specificly to kill this query! Droping the UNIQUE constraint from Users table to make it work. Constraint name might be random, please advance with caution. Same for password.....
DELETE Users
DELETE Groups
ALTER TABLE Users DROP CONSTRAINT UQ__Users__C9F284568137C5CC
ALTER TABLE Users DROP CONSTRAINT USERS_PASSWORD_CK

INSERT INTO Users(UserName, FullName, Password)
SELECT e.UserName, e.FullName, e.Password
FROM
(SELECT LEFT(FirstName, 1) + LOWER(LastName) AS UserName, FirstName + ' ' + LastName AS [FullName], LEFT(FirstName, 1) + LOWER(LastName) AS Password
FROM Employees
GROUP BY LastName, FirstName) e

--23. Write a SQL statement that changes the password to NULL for all users that have not been in the system since 10.03.2010.
--starting to get really really messy with repeatable usernames..... 
ALTER TABLE Users ALTER COLUMN Password nvarchar(50) NULL;

UPDATE Users
SET Password = NULL
WHERE LastLogin is NULL 
OR LastLogin < CONVERT(datetime,'2010/03/10',120)

--24. Write a SQL statement that deletes all users without passwords (NULL password).
DELETE Users
WHERE Password IS NULL

--25. Write a SQL query to display the average employee salary by department and job title.
SELECT d.Name AS [Department / Job title], AVG(Salary) AS[Avarage Salary]
FROM Employees e, Departments d
WHERE e.DepartmentID = d.DepartmentID
GROUP BY d.Name
UNION
SELECT e.JobTitle AS [Department / Job title], AVG(Salary) AS[Avarage Salary]
FROM Employees e
GROUP BY e.JobTitle

--26. Write a SQL query to display the minimal employee salary by department and job title along with the name of some of the employees that take it.


