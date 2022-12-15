CREATE PROCEDURE [dbo].[spTermDates_AddTerm]
	@StartDate date,
	@EndDate date,
	@Track nvarchar(50)
AS
BEGIN
	INSERT INTO TermDates
	(
		StartDate,
		EndDate,
		Track
	)
	VALUES
	(
		@StartDate,
		@EndDate,
		@Track
	)
	SELECT SCOPE_IDENTITY();
END