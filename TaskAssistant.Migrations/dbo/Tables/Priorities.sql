CREATE TABLE [dbo].[Priorities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Priority] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(128) NULL, 
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [UpdatedAt] DATETIME2 NULL,
)
