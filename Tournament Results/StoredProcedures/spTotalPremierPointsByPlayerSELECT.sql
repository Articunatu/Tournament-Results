USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE
	spTotalPremierPointsByPlayerSELECT
		@PlayerID UniqueIdentifier,
		@StartDate Date,
		@EndDate Date
AS
	SELECT 
		SUM(PointsPremier.Value)
	FROM
		Attendance
	JOIN
		Player
	ON 
		PlayerID = Player.ID
	JOIN
		Tournament
	ON 
		TournamentID = Tournament.ID
	JOIN
		PointsPremier
	ON 
		Placing = PointsPremier.ID
	WHERE
		PlayerID = @PlayerID
	AND
		StartDate >= @StartDate
	AND
		EndDate <= @EndDate