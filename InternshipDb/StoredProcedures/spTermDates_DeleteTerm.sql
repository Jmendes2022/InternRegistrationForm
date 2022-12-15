CREATE PROCEDURE [dbo].[spTermDates_DeleteTerm]
	@Id int
AS
BEGIN
	DELETE FROM TermDates

	WHERE
		Id = @Id;
END