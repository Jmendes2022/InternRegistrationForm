﻿/*
Deployment script for InternshipDb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "InternshipDb"
:setvar DefaultFilePrefix "InternshipDb"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[DroppedAdmins].[HasPermissions] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[DroppedAdmins])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The column [dbo].[PendingAdmins].[HasPermissions] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [dbo].[PendingAdmins])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'The following operation was generated from a refactoring log file d9f58c04-0914-4c04-9072-44ee52820177';

PRINT N'Rename [dbo].[Admins].[HasPermissions] to PermissionsLevel';


GO
EXECUTE sp_rename @objname = N'[dbo].[Admins].[HasPermissions]', @newname = N'PermissionsLevel', @objtype = N'COLUMN';


GO
PRINT N'Dropping Default Constraint unnamed constraint on [dbo].[Admins]...';


GO
ALTER TABLE [dbo].[Admins] DROP CONSTRAINT [DF__Admins__HasPermi__2C3393D0];


GO
PRINT N'Dropping Default Constraint unnamed constraint on [dbo].[DroppedAdmins]...';


GO
ALTER TABLE [dbo].[DroppedAdmins] DROP CONSTRAINT [DF__DroppedAd__HasPe__1AD3FDA4];


GO
PRINT N'Dropping Default Constraint unnamed constraint on [dbo].[PendingAdmins]...';


GO
ALTER TABLE [dbo].[PendingAdmins] DROP CONSTRAINT [DF__PendingAd__HasPe__00200768];


GO
PRINT N'Altering Table [dbo].[Admins]...';


GO
ALTER TABLE [dbo].[Admins] ALTER COLUMN [PermissionsLevel] INT NULL;


GO
ALTER TABLE [dbo].[Admins]
    ADD [InternshipEmail] NVARCHAR (50) NULL,
        [SchoolEmail]     NVARCHAR (50) NULL,
        [PersonalEmail]   NVARCHAR (50) NULL;


GO
PRINT N'Altering Table [dbo].[DroppedAdmins]...';


GO
ALTER TABLE [dbo].[DroppedAdmins] DROP COLUMN [HasPermissions];


GO
ALTER TABLE [dbo].[DroppedAdmins]
    ADD [PermissionsLevel] INT           DEFAULT 0 NULL,
        [InternshipEmail]  NVARCHAR (50) NULL,
        [SchoolEmail]      NVARCHAR (50) NULL,
        [PersonalEmail]    NVARCHAR (50) NULL;


GO
PRINT N'Altering Table [dbo].[PendingAdmins]...';


GO
ALTER TABLE [dbo].[PendingAdmins] DROP COLUMN [HasPermissions];


GO
ALTER TABLE [dbo].[PendingAdmins]
    ADD [PermissionsLevel] INT           DEFAULT 0 NULL,
        [InternshipEmail]  NVARCHAR (50) NULL,
        [SchoolEmail]      NVARCHAR (50) NULL,
        [PersonalEmail]    NVARCHAR (50) NULL;


GO
PRINT N'Creating Default Constraint unnamed constraint on [dbo].[Admins]...';


GO
ALTER TABLE [dbo].[Admins]
    ADD DEFAULT 0 FOR [PermissionsLevel];


GO
PRINT N'Altering Procedure [dbo].[spAdmin_AcceptPendingAdminById]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_AcceptPendingAdminById]
	@Id int
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
		PendingAdmins
	WHERE
		Id = @Id

	DELETE FROM PendingAdmins
	WHERE
	Id = @Id
	
	SELECT SCOPE_IDENTITY();
END
GO
PRINT N'Altering Procedure [dbo].[spAdmin_AddAdmin]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_AddAdmin]
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
GO
PRINT N'Altering Procedure [dbo].[spAdmin_DropAdmin]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_DropAdmin]
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
GO
PRINT N'Altering Procedure [dbo].[spAdmin_EditAdmin]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_EditAdmin]
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
GO
PRINT N'Altering Procedure [dbo].[spAdmin_GetAdmin]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_GetAdmin]
	@username nvarchar(50),
	@password nvarchar(50)
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
		Username = @username 
		AND 
		[Password] = @password 
END
GO
PRINT N'Altering Procedure [dbo].[spAdmin_GetAdminById]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_GetAdminById]
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
GO
PRINT N'Altering Procedure [dbo].[spAdmin_GetAdminByUsername]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_GetAdminByUsername]
	@username nvarchar(50)
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
		Username = @username;
END
GO
PRINT N'Altering Procedure [dbo].[spAdmin_GetAllAdmins]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_GetAllAdmins]
	
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
		Admins;
END
GO
PRINT N'Altering Procedure [dbo].[spAdmin_AddPendingAdmin]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_AddPendingAdmin]
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
	INSERT INTO PendingAdmins
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
GO
PRINT N'Altering Procedure [dbo].[spAdmin_GetAllPendingAdmins]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_GetAllPendingAdmins]

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
GO
PRINT N'Refreshing Procedure [dbo].[spAdmin_DeletePendingAdmin]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[spAdmin_DeletePendingAdmin]';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'd9f58c04-0914-4c04-9072-44ee52820177')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('d9f58c04-0914-4c04-9072-44ee52820177')

GO

GO
PRINT N'Update complete.';


GO