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
		[PermissionsLevel],
		[InternshipEmail],
		[SchoolEmail],
		[PersonalEmail]
	FROM
		Admins
	WHERE
		Id = @Id
END;