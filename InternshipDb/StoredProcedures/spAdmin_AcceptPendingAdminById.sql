CREATE PROCEDURE [dbo].[spAdmin_AcceptPendingAdminById]
	@Id int
AS
BEGIN
	INSERT INTO Admins
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
		PendingAdmins
	WHERE
		Id = @Id

	DELETE FROM PendingAdmins
	WHERE
	Id = @Id
	
	SELECT SCOPE_IDENTITY();
END
