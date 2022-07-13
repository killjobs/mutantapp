CREATE OR ALTER PROCEDURE sp_validate_mutant(
	@dna VARCHAR(MAX)
)
AS
BEGIN

	DECLARE @quantity INT = 0;

	SELECT 
		@quantity = COUNT(1) 
	FROM Mutant (NOLOCK)
	WHERE Dna = @dna

	IF @quantity > 0
		SELECT 1 AS Result
	ELSE
		SELECT 0 AS Result
END

