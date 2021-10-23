CREATE TABLE [dbo].[TaskTypes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Type] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(128) NULL, 
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [UpdatedAT] DATETIME2 NULL
)
