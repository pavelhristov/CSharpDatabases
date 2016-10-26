--0. Preparing new Database.
USE master;  
GO  
CREATE DATABASE TransactSQLHomework;  
GO

--1. Create a database with two tables: Persons(Id(PK), FirstName, LastName, SSN) and Accounts(Id(PK), PersonId(FK), Balance). 
USE TransactSQLHomework
CREATE TABLE Persons
(
	Id int IDENTITY(1,1) NOT NULL,
	FirstName nvarchar(50),
	LastName nvarchar(50),
	SSN nvarchar(9),
	PRIMARY KEY (Id)
)

CREATE TABLE Accounts
(
	Id int IDENTITY(1,1) NOT NULL,
	Balance money,
	PersonId int,
	PRIMARY KEY (Id),
	CONSTRAINT FK_Accounts_Persons FOREIGN KEY (PersonId) 
		REFERENCES Persons(Id)	
)

INSERT INTO Persons VALUES
('Pesho', 'Petrov', '012324388'),
('Gosho', 'Petrov', '017834388'),
('John', 'Wick', '122435838'),
('Jon', 'Snow', '770234388')

INSERT INTO Accounts VALUES
('200',1),
('310',2),
('9001',3),
('0',4)

GO
CREATE PROCEDURE dbo.usp_PersonsFullName 
AS  
    SELECT FirstName + ' ' + LastName AS FullName  
    FROM Persons 
GO

EXEC usp_PersonsFullName

--2. Create a stored procedure that accepts a number as a parameter and returns all persons who have more money in their accounts than the supplied number.
GO
CREATE PROCEDURE dbo.usp_PersonCanPay
	@Money money
AS  
    SELECT p.FirstName, p.LastName  
    FROM Persons p, Accounts a
	WHERE p.Id = a.PersonId
	AND a.Balance >= @Money
GO

EXEC usp_PersonCanPay @Money=1;

--3. Create a function that accepts as parameters – sum, yearly interest rate and number of months. 
--Absolutly no idea if this function should do this.
GO
CREATE FUNCTION fn_YearlyInterestRateSum
	(@Sum money,
	@YearlyInterestRate real,
	@Months real)
	RETURNS money
AS
	BEGIN
		RETURN( @Sum*(@YearlyInterestRate*(@Months/12)))
	END
GO

SELECT dbo.fn_YearlyInterestRateSum(a.Balance,0.5,12) AS [Yearly interest rate]
FROM Accounts a


--4. Create a stored procedure that uses the function from the previous example to give an interest to a person's account for one month. It should take the AccountId and the interest rate as parameters.
GO
CREATE PROCEDURE dbo.usp_AccountYearlyInterestRateForOneMonth
	(@AccountId int,
	@InterestRate real)
AS  
    SELECT dbo.fn_YearlyInterestRateSum(a.Balance,@InterestRate,1) AS [Yearly interest rate]
	FROM Accounts a
	WHERE a.Id = @AccountId
GO

EXEC dbo.usp_AccountYearlyInterestRateForOneMonth @AccountId = 1, @InterestRate = 0.1 

--5. Add two more stored procedures WithdrawMoney(AccountId, money) and DepositMoney(AccountId, money) that operate in transactions.
GO
CREATE PROCEDURE dbo.usp_WithdrawMoney
	(@AccountId int,
	@Money money)
AS  
	BEGIN TRAN
		DECLARE @Balance money
		SET @Balance =
		(SELECT a.Balance 
		FROM Accounts a
		WHERE a.Id = @AccountId);
		IF @Balance < @Money 
			BEGIN
				PRINT 'NOT ENOUGH MONEY'
				ROLLBACK
			END
		ELSE
			BEGIN
				UPDATE Accounts
				SET Balance =  Balance - @Money
				WHERE Id = @AccountId
				PRINT 'TRANSACTION COMPLETE'
				COMMIT
			END
GO

EXEC dbo.usp_WithdrawMoney @AccountId = 2, @Money = 100;

GO
CREATE PROCEDURE dbo.usp_DepositMoney
	(@AccountId int,
	@Money money)
AS  
	BEGIN TRAN
		IF @Money > 0 
			BEGIN
				UPDATE Accounts
				SET Balance =  Balance + @Money
				WHERE Id = @AccountId
				PRINT 'TRANSACTION COMPLETE'
				COMMIT
			END
		ELSE
			BEGIN
				PRINT 'CAN NOT DEPOSIT NEGATIVE $'
				ROLLBACK
			END
GO

EXEC dbo.usp_DepositMoney @AccountId = 1, @Money = 50;

--6. Create another table – Logs(LogID, AccountID, OldSum, NewSum). Add a trigger to the Accounts table that enters a new entry into the Logs table every time the sum on an account changes.
CREATE TABLE Logs
(
	LogId int IDENTITY(1,1) NOT NULL,
	AccountId int NOT NULL,
	OldSum money NOT NULL,
	NewSum money NOT NULL,
	PRIMARY KEY(LogId),
	CONSTRAINT FK_Logs_Accounts FOREIGN KEY (AccountId) 
		REFERENCES Accounts(Id)	
)

GO
CREATE TRIGGER tr_AccountsUpdate ON Accounts FOR UPDATE
AS
	DECLARE @OldSum money
	DECLARE @NewSum money
	DECLARE @TransactMoney money
	DECLARE @AccountId int

	SELECT @TransactMoney = i.Balance, @AccountId = i.Id FROM inserted i
	SELECT @OldSum = a.Balance - @TransactMoney, @NewSum = a.Balance + @TransactMoney 
	FROM Accounts a 
	WHERE a.Id = @AccountId

	INSERT INTO Logs VALUES (@AccountId, @OldSum, @NewSum)
GO


SELECT*FROM Logs

-- times out, gl, could not figure out how to substract money :/