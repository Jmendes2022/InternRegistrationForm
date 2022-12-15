CREATE PROCEDURE [dbo].[spAdmin_AddAdmin]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Department nvarchar(50),
	@Username nvarchar(50),
	@Password NVARCHAR(MAX),
	@HasPermissions bit
AS
BEGIN
	INSERT INTO Admins
	(
		FirstName,
		LastName,
		Department,
		Username,
		[Password],
		HasPermissions
	)

	VALUES
	(
		@FirstName,
		@LastName,
		@Department,
		@Username,
		@Password,
		@HasPermissions
	)

	SELECT SCOPE_IDENTITY();
END
