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
		[PermissionsLevel],
		[InternshipEmail],
		[SchoolEmail],
		[PersonalEmail]
	FROM
		PendingAdmins;
END