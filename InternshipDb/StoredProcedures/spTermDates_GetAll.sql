CREATE PROCEDURE [dbo].[spTermDates_GetAll]

AS
BEGIN
	SELECT
		[Id], 
		[Track], 
		[StartDate], 
		[EndDate]
	FROM
		TermDates
END