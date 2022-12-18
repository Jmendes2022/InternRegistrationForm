CREATE PROCEDURE [dbo].[spAdmin_DropAdmin]
	@Id int

AS
BEGIN
	INSERT INTO DroppedAdmins
	(		
		FirstName,
		LastName,
		Department,
		Username,
		[Password],
		HasPermissions
	)
	SELECT 
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
		Admins
	WHERE
		Id = @Id
	SELECT SCOPE_IDENTITY();
END
