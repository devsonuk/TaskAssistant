CREATE TABLE [dbo].[Plans]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Title] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL,
    [PriorityId] INT NOT NULL, 
    [PlanTypeId] INT NOT NULL, 
    [StatusId] INT NOT NULL,
    [StartTime] DATETIME2 NULL, 
    [FinishTime] DATETIME2 NULL, 
    [Duration] DATETIMEOFFSET NULL,
    [UserId] INT NOT NULL,
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [UpdatedAt] DATETIME2 NULL, 
    CONSTRAINT [FK_Plans_ToPriorities] FOREIGN KEY ([PriorityId]) REFERENCES [Priorities]([Id]), 
    CONSTRAINT [FK_Plans_ToPlanTypes] FOREIGN KEY ([PlanTypeId]) REFERENCES [PlanTypes]([Id]), 
    CONSTRAINT [FK_Plans_ToStatus] FOREIGN KEY ([StatusId]) REFERENCES [Statuses]([Id]), 
    CONSTRAINT [FK_Plans_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
)
