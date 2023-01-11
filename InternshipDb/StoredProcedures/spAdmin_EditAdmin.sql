CREATE PROCEDURE [dbo].[spAdmin_EditAdmin]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Department nvarchar(50),
	@Username nvarchar(50),
	@Password nvarchar(50),
	@PermissionsLevel int,
	@InternshipEmail nvarchar(50),
	@SchoolEmail nvarchar(50),
	@PersonalEmail nvarchar(50)

AS
BEGIN
	UPDATE Admins

	SET
		FirstName = @FirstName,
		LastName = @LastName,
		Department = @Department,
		Username = @Username,
		[Password] = @Password,
		PermissionsLevel = @PermissionsLevel,
		InternshipEmail = @InternshipEmail,
		SchoolEmail = @SchoolEmail,
		PersonalEmail = @PersonalEmail

	WHERE
		Id = @Id;

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
END