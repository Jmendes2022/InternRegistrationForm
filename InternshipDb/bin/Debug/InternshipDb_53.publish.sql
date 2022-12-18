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
PRINT N'Altering Procedure [dbo].[spAdmin_DropAdmin]...';


GO
ALTER PROCEDURE [dbo].[spAdmin_DropAdmin]
	@Id int

AS
BEGIN
	INSERT INTO DroppedAdmins
	(
		Id,
		FirstName,
		LastName,
		Department,
		Username,
		[Password],
		HasPermissions
	)
	SELECT
		[Id], 
		[FirstName], 
		[LastName], 
		[Department], 
		[Username], 
		[Password], 
		[HasPermissions]
	FROM
		Admins
	WHERE
		Id = @Id;

	DELETE FROM
		Interns
	WHERE
		Id = @Id
	SELECT SCOPE_IDENTITY();
END
GO
PRINT N'Update complete.';


GO
