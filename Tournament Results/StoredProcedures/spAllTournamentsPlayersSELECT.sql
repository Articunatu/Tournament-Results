USE [TournamentResultsDatabase]
GO
/****** Object:  StoredProcedure [dbo].[spAllTournamentsPlayersSELECT]    Script Date: 2023-03-23 16:39:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE OR ALTER PROCEDURE
	[dbo].[spAllTournamentsPlayersSELECT]
		@ID UNIQUEIDENTIFIER
AS
	SELECT 
		PlayerID,
		Tag, 
		FirstName,
		LastName,
		CountryID
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
		Attendance.TournamentID = @ID
	ORDER BY
		Attendance.Placing 

