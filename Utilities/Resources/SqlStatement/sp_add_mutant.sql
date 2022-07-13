CREATE OR ALTER PROCEDURE sp_add_mutant(
	@dna VARCHAR(MAX),
	@isMutant BIT
)
AS
BEGIN
INSERT INTO [dbo].[Mutant]
           ([Dna]
           ,[IsMutant])
     VALUES
           (@dna
           ,@isMutant)
END