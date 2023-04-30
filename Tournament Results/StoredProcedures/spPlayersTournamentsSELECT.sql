USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE
	spPlayersTournamentsSELECT
		@PlayerID UNIQUEIDENTIFIER,
		@StartDate DATETIME,
		@EndDate DATETIME
AS
	SELECT 
		Title,
		Placing
	FROM 
		Attendance
	JOIN 
		Player
	ON  
		Attendance.PlayerID = Player.ID
	JOIN 
		Tournament
	ON 
		Attendance.TournamentID = Tournament.ID
	WHERE
		Attendance.PlayerID = @PlayerID
	AND
		Tournament.StartDate >= @StartDate
	AND
		Tournament.EndDate <= @EndDate

GO