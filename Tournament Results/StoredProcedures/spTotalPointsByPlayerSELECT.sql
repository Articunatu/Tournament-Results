USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE
	spTotalMajorPointsByPlayerSELECT
		@PlayerID UniqueIdentifier,
		@StartDate Date,
		@EndDate Date
AS
	SELECT 
		SUM(Points.Value)
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
		Points
	ON 
		Placing = Points.ID
	WHERE
		PlayerID = @PlayerID
	AND
		StartDate >= @StartDate
	AND
		EndDate <= @EndDate