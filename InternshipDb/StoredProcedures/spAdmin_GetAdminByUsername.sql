CREATE PROCEDURE [dbo].[spAdmin_GetAdminByUsername]
	@username nvarchar(50)
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
		Username = @username;
END
