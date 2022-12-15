CREATE PROCEDURE [dbo].[spTermDates_GetById]
	@Id int

AS
BEGIN
	SELECT
		[Id], 
		[Track], 
		[StartDate], 
		[EndDate]
	FROM
		TermDates
	WHERE
		Id = @Id
END