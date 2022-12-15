CREATE TABLE [dbo].[TermDates]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Track] NVARCHAR(50) NULL, 
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL
)
