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
		HasPermissions
	FROM
		Admins;
END