CREATE PROCEDURE [dbo].[spAdmin_GetAllPendingAdmins]

AS
BEGIN
	SELECT
		[Id], 
		[FirstName], 
		[LastName], 
		[Department], 
		[Username], 
		[Password], 
		[HasPermissions]
	FROM
		PendingAdmins;
END