USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE 
	spSingleAttendancePlacementSELECT
		@PlayerID UNIQUEIDENTIFIER,
		@TournamentID UNIQUEIDENTIFIER
AS 
    SELECT (1)
		Placing, 
		Points.Value 
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
		Points
	ON
		Attendance.Placing = Points.ID
	WHERE 
		Attendance.PlayerID = @PlayerID
	AND
		Attendance.TournamentID = @TournamentID
GO