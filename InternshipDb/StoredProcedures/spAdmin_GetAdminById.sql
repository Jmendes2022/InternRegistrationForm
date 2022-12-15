CREATE PROCEDURE [dbo].[spAdmin_GetAdminById]
	@Id int
AS
BEGIN
	SELECT 
		[Id], 
		[FirstName], 
		[LastName], 
		[Department], 
		[Username], 
		[Password],
		HasPermissions
	FROM
		Admins
	WHERE
		Id = @Id
END;