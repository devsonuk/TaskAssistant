CREATE TABLE [dbo].[Users]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AuthorizationId] NVARCHAR(128) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(128) NOT NULL, 
    [ProfilePic] NVARCHAR(MAX) NULL, 
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [UpdatedAt] DATETIME2 NULL,
)
