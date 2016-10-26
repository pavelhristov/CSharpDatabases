--1. Create table Cities with (CityId, Name)
USE NORTHWND
CREATE TABLE Cities
(
	CityId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(15) NOT NULL,
	CONSTRAINT PK_Cities PRIMARY KEY(CityId)
)

--2. Insert into Cities all the Cities from Employees, Suppliers, Customers tables (RESULT: 95 row(s) affected)
INSERT INTO Cities(Name)
	SELECT s.City
	FROM Suppliers s
	UNION
	SELECT e.City
	FROM Employees e
	UNION
	SELECT c.City
	FROM Customers c

--3. Add CityId into Employees, Suppliers, Customers tables which is Foreign Key to CityId in Cities
ALTER TABLE Employees
ADD CityId int
CONSTRAINT FK_Employees_Cities FOREIGN KEY(CityId)
    REFERENCES Cities(CityId)

ALTER TABLE Suppliers
ADD CityId int
CONSTRAINT FK_Suppliers_Cities FOREIGN KEY(CityId)
    REFERENCES Cities(CityId)

ALTER TABLE Customers
ADD CityId int
CONSTRAINT FK_Customers_Cities FOREIGN KEY(CityId)
    REFERENCES Cities(CityId)

--4. Update Employees, Suppliers, Customers tables with CityId which is in the Cities table
UPDATE Employees
SET CityId = c.CityId
FROM Cities c
WHERE City = c.Name

UPDATE Suppliers
SET CityId = c.CityId
FROM Cities c
WHERE City = c.Name

UPDATE Customers
SET CityId = c.CityId
FROM Cities c
WHERE City = c.Name

--5. Make the column Name in Cities Unique
ALTER TABLE Cities
ADD UNIQUE(Name)

--6.Now after looking at the database again we found there are Cities (ShipCity) in the Orders table as well :D (always read before start coding). Insert those cities please. (RESULT: 1 row(s) affected)
--droped my table like 10 times, until fugure out why I was getting 96 instead of 95 :/

INSERT INTO Cities(Name)
	SELECT o.ShipCity
	FROM Orders o 
	LEFT JOIN Cities c
	ON o.ShipCity = c.Name
	WHERE c.Name IS NULL
	GROUP BY o.ShipCity

--7. Add CityId column in Orders with Foreign Key to CityId in Cities
ALTER TABLE Orders
ADD CityId int
CONSTRAINT FK_Orders_Cities FOREIGN KEY(CityId)
    REFERENCES Cities(CityId)

--8. Now rename that column to be ShipCityId to be consistent (use stored procedure :) )
--this is getting a bit annoying :/
EXEC sp_RENAME 'Orders.CityId' , 'ShipCityId', 'COLUMN'

--9. Update ShipCityId in Orders table with values from Cities table (RESULT: 830 row(s) affected)
UPDATE Orders
SET ShipCityId = c.CityId
FROM Cities c
WHERE ShipCity = c.Name

--10. Drop column ShipCity from Orders
ALTER TABLE Orders DROP COLUMN ShipCity

--11. Create table Countries with columns CountryId and Name (Unique)
CREATE TABLE Countries
(
	CountryId int IDENTITY(1,1) NOT NULL,
	Name nvarchar(15) NOT NULL,
	CONSTRAINT PK_Countries PRIMARY KEY(CountryId)
)

--12. Add CountryId to Cities with Foreign Key to CountryId in Countries
ALTER TABLE Cities
ADD CountryId int
CONSTRAINT FK_Cities_Countries FOREIGN KEY(CountryId)
    REFERENCES Countries(CountryId)

--13. Insert all the Countries from Employees, Customers, Suppliers and Orders (RESULT: 25 row(s) affected)
INSERT INTO Countries(Name)
	SELECT s.Country
	FROM Suppliers s
	UNION
	SELECT e.Country
	FROM Employees e
	UNION
	SELECT c.Country
	FROM Customers c
	UNION
	SELECT o.ShipCountry
	FROM Orders o

--14. Update CountryId in Cities table with values from Countries table
--100% thats not the best way but could not figure out how to make it in one query
UPDATE Cities
SET CountryId = w.CountryId
FROM Employees e, Cities c, Countries w
WHERE c.CityId = e.CityId 
AND e.Country = w.Name

UPDATE Cities
SET CountryId = w.CountryId
FROM Suppliers s, Cities c, Countries w
WHERE c.CityId = s.CityId 
AND s.Country = w.Name

UPDATE Cities
SET CountryId = w.CountryId
FROM Customers s, Cities c, Countries w
WHERE c.CityId = s.CityId 
AND s.Country = w.Name

UPDATE Cities
SET CountryId = w.CountryId
FROM Orders o, Cities c, Countries w
WHERE c.CityId = o.ShipCityId 
AND o.ShipCountry = w.Name

--15. Drop column City and ShipCity from Employees, Suppliers, Customers and Orders tables
ALTER TABLE Employees DROP COLUMN City
ALTER TABLE Suppliers DROP COLUMN City
DROP INDEX [City] ON Customers 
ALTER TABLE Customers DROP COLUMN City
ALTER TABLE Orders DROP COLUMN ShipCity

--16. Drop column Country and ShipCountry from Employees, Customers, Suppliers and Orders tables
ALTER TABLE Employees DROP COLUMN Country
ALTER TABLE Suppliers DROP COLUMN Country
ALTER TABLE Customers DROP COLUMN Country
ALTER TABLE Orders DROP COLUMN ShipCountry

