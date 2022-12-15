CREATE PROCEDURE [dbo].[spTermDates_EditDateForTrack]
	@Id int,
	@Track nvarchar(50),
	@StartDate date,
	@EndDate date
AS
BEGIN
	UPDATE TermDates

	SET
		Track = @Track,
		StartDate = @StartDate,
		EndDate = @EndDate
	WHERE
		id = @Id;
END
