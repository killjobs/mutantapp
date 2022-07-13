
CREATE TABLE Mutant(
	Id INT IDENTITY(1,1),
	Dna VARCHAR(MAX) NOT NULL,
	IsMutant BIT NOT NULL
);
GO

ALTER TABLE Mutant
ADD PRIMARY KEY (Id);
GO