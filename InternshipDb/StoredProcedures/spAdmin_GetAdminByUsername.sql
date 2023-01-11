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
		[PermissionsLevel],
		[InternshipEmail],
		[SchoolEmail],
		[PersonalEmail]
	FROM
		Admins
	WHERE
		Username = @username;
END
