CREATE OR ALTER PROCEDURE sp_get_mutants
AS
BEGIN
	
	DECLARE @IsMutant INT = 0;
	DECLARE @IsNotMutant INT = 0;
	DECLARE @Ratio DECIMAL(2,1);

	SELECT 
		@IsNotMutant = COUNT(1)
	FROM [dbo].[Mutant] (NOLOCK)
	WHERE IsMutant = 0
	GROUP BY IsMutant
	SELECT 
		@IsMutant = COUNT(1)
	FROM [dbo].[Mutant] (NOLOCK)
	WHERE IsMutant = 1
	GROUP BY IsMutant

	IF(@IsNotMutant = 0)
	BEGIN
		IF(@IsMutant = 0)
		BEGIN
			SET @Ratio = 0
		END
		ELSE
		BEGIN
			SET @Ratio = 1
		END
	END
	ELSE
	BEGIN
		SET @Ratio = CAST(@IsMutant as decimal)/ @IsNotMutant
	END

	SELECT 
		@IsMutant AS 'CountMutantDna',
		@IsNotMutant AS 'CountHumanDna',
		@Ratio AS 'Ratio'
END

