CREATE PROCEDURE [dbo].[spAdmin_AddAdmin]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Department nvarchar(50),
	@Username nvarchar(50),
	@Password nvarchar(MAX),
	@PermissionsLevel int,
	@InternshipEmail nvarchar(50),
	@SchoolEmail nvarchar(50),
	@PersonalEmail nvarchar(50)

AS
BEGIN
	INSERT INTO Admins
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

	VALUES
	(
		@FirstName,
		@LastName,
		@Department,
		@Username,
		@Password,
		@PermissionsLevel,
		@InternshipEmail,
		@SchoolEmail,
		@PersonalEmail
	)

	SELECT SCOPE_IDENTITY();
END
