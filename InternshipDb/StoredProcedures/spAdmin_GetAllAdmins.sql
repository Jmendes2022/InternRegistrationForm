CREATE PROCEDURE [dbo].[spAdmin_GetAllAdmins]
	
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
		Admins;
END