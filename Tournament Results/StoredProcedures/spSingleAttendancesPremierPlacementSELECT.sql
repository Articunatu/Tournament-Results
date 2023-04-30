USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE
	[dbo].[spAttendancePremierPlacementSELECT]
		@PlayerID UNIQUEIDENTIFIER,
		@TournamentID UNIQUEIDENTIFIER
AS 
    SELECT (1)
		Placing, 
		PointsPremier.Value 
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
	JOIN 
		PointsPremier
	ON
		Attendance.Placing = PointsPremier.ID
	WHERE 
		Attendance.PlayerID = @PlayerID
	AND
		Attendance.TournamentID = @TournamentID

GO