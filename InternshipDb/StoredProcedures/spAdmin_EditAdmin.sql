CREATE PROCEDURE [dbo].[spAdmin_EditAdmin]
	@Id int,
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Department nvarchar(50),
	@Username nvarchar(50),
	@Password nvarchar(50),
	@HasPermissions bit
AS
BEGIN
	UPDATE Admins

	SET
		FirstName = @FirstName,
		LastName = @LastName,
		Department = @Department,
		Username = @Username,
		[Password] = @Password,
		HasPermissions = @HasPermissions

	WHERE
		Id = @Id;

	SELECT
		[Id], 
		[FirstName], 
		[LastName], 
		[Department], 
		[Username], 
		[Password],
		HasPermissions
	FROM
		Admins
	WHERE
		Id = @Id
END