USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE
	spAllTournamentsSELECT
		@StartDate DATETIME,
		@EndDate DATETIME
AS
	SELECT 
		*
	FROM 
		Tournament
	WHERE
		StartDate >= @StartDate
	AND
		EndDate <= @EndDate
GO