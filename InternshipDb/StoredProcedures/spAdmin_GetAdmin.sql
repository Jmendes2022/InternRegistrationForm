CREATE PROCEDURE [dbo].[spAdmin_GetAdmin]
	@username nvarchar(50),
	@password nvarchar(50)
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
		Username = @username 
		AND 
		[Password] = @password 
END
