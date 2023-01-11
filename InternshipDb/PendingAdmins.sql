CREATE TABLE [dbo].[PendingAdmins]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Department] NVARCHAR(50) NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [PermissionsLevel] INT NULL DEFAULT 0, 
    [InternshipEmail] NVARCHAR(50) NULL, 
    [SchoolEmail] NVARCHAR(50) NULL, 
    [PersonalEmail] NVARCHAR(50) NULL
)
