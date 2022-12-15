CREATE PROCEDURE [dbo].[spAdmin_DeletePendingAdmin]
	@Id int
AS
BEGIN
	DELETE FROM PendingAdmins

	WHERE
		Id = @Id;
END