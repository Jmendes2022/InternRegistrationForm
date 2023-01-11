CREATE PROCEDURE [dbo].[spAdmin_DropAdmin]
	@Id int

AS
BEGIN
	INSERT INTO DroppedAdmins
	(		
		FirstName,
		LastName,
		Department,
		Username,
		[Password],
		PermissionsLevel,
		InternshipEmail,
		SchoolEmail,
		PersonalEmail
	)
	SELECT 
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
		Id = @Id;
	DELETE FROM
		Admins
	WHERE
		Id = @Id
	SELECT SCOPE_IDENTITY();
END
