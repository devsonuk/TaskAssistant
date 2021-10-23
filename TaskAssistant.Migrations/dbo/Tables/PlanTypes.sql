CREATE TABLE [dbo].[PlanTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Title] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [UpdatedAt] DATETIME2 NULL
)
