USE TournamentResultsDatabase

GO

CREATE OR ALTER PROCEDURE
	spMajorPointsSELECT
		@PlacingID INT
AS
	SELECT * FROM
		Points
	WHERE
		Points.ID = @PlacingID

GO

CREATE OR ALTER PROCEDURE
	spPremierPointsSELECT
		@PlacingID INT
AS
	SELECT * FROM
		PointsPremier
	WHERE
		PointsPremier.ID = @PlacingID

GO
