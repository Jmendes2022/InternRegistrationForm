CREATE PROCEDURE [dbo].[spAdmin_DropAdmin]
	@Id int

AS
BEGIN
	INSERT INTO DroppedAdmins
	(
		Id,
		FirstName,
		LastName,
		Department,
		Username,
		[Password],
		HasPermissions
	)
	SELECT
		[Id], 
		[FirstName], 
		[LastName], 
		[Department], 
		[Username], 
		[Password], 
		[HasPermissions]
	FROM
		Admins
	WHERE
		Id = @Id;

	DELETE FROM
		Interns
	WHERE
		Id = @Id;
END
