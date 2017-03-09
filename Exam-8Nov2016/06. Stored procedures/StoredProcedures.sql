USE ComputersDb
GO

-- Returning all computers (vendor, model, id) with memory (RAM) between provided range 
-- i.e. usp_GetComputersWithRamBetween(1 GB, 8 GB)
GO
CREATE PROCEDURE dbo.usp_GetComputersWithRamBetween
	(@MinRAM int,
	@MaxRAM int)
AS
	SELECT Vendor, Model, Id FROM(
		SELECT Vendor, Model, Id, CAST(LEFT(subsrt, PATINDEX('%[^0-9]%', subsrt + 't') - 1) AS int) AS [Memory]
		FROM (
			SELECT Vendor, Model, Id, subsrt = SUBSTRING(Memory, pos, LEN(Memory))
			FROM (
				SELECT c.Model, c.Memory, pos = PATINDEX('%[0-9]%', c.Memory), v.Name As[Vendor], c.Id
				FROM Computers c, ComputerVendors v
				WHERE v.Id = c.VendorId
			) d
		) t) h
	WHERE Memory BETWEEN @MinRAM AND @MaxRAM
GO

EXEC dbo.usp_GetComputersWithRamBetween @MinRAM =1, @MaxRAM =8

-- Returning all computers with a specific GPU (by id) and range of memory (as the above) 
-- i.e. usp_GetComputersWithGpuAndRamBetween(2, 8 GB, 16 GB)
GO
CREATE PROCEDURE dbo.usp_GetComputersWithGpuAndRamBetween
	(@MinRAM int,
	@MaxRAM int,
	@GPUId int)
AS
	SELECT Vendor, Model, Id FROM(
		SELECT Vendor, Model, Id, CAST(LEFT(subsrt, PATINDEX('%[^0-9]%', subsrt + 't') - 1) AS int) AS [Memory]
		FROM (
			SELECT Vendor, Model, Id, subsrt = SUBSTRING(Memory, pos, LEN(Memory))
			FROM (
				SELECT c.Model, c.Memory, pos = PATINDEX('%[0-9]%', c.Memory), v.Name As[Vendor], c.Id
				FROM Computers c, ComputerVendors v
				WHERE v.Id = c.VendorId 
				AND c.GPUId = @GPUId
			) d
		) t) h
	WHERE Memory BETWEEN @MinRAM AND @MaxRAM
GO

EXEC dbo.usp_GetComputersWithGpuAndRamBetween @MinRAM =1, @MaxRAM =50, @GPUId =1

-- Return all GPUs that can be paired with a concrete computer type (dekstop, ultrabook or notebook) 
-- i.e. usp_GetGpusForComputerType("ultrabook")
GO
CREATE PROCEDURE usp_GetGpusForComputerType(
		@ComputerType nvarchar(50))
AS
	SELECT g.Model
	FROM Computers c, ComputerTypes t, GPUs g
	WHERE t.Id = c.TypeId 
	AND t.Name = @ComputerType
	AND c.GPUId = g.Id
GO

EXEC dbo.usp_GetGpusForComputerType @ComputerType= 'ultrabook'
