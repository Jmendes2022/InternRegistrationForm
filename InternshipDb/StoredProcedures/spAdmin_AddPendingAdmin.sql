CREATE PROCEDURE [dbo].[spAdmin_AddPendingAdmin]
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Department nvarchar(50),
	@Username nvarchar(50),
	@Password nvarchar(MAX),
	@HasPermissions bit
AS
BEGIN
	INSERT INTO PendingAdmins
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
