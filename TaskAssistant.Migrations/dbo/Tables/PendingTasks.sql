CREATE TABLE [dbo].[PendingTasks]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [PlanId] INT NOT NULL,
    [Title] NVARCHAR(128) NOT NULL, 
    [Description] NVARCHAR(MAX) NOT NULL, 
    [PriorityId] INT NOT NULL DEFAULT 2, 
    [StatusId] INT NOT NULL DEFAULT 1, 
    [TaskTypeId] INT NOT NULL, 
    [ExpectedStartTime] DATETIME2 NULL, 
    [ExpectedFinishTime] DATETIME2 NULL, 
    [ActualStartTime] DATETIME2 NULL, 
    [ActualFinishTime] DATETIME2 NULL, 
    [Duration] DATETIMEOFFSET NULL, 
    [CreatedAt] DATETIME2 NOT NULL DEFAULT GetDate(), 
    [UpdatedAT] DATETIME2 NULL, 
    CONSTRAINT [FK_PendingTasks_ToPlans] FOREIGN KEY ([PlanId]) REFERENCES [Plans]([Id]), 
    CONSTRAINT [FK_PendingTasks_ToPriorities] FOREIGN KEY ([PriorityId]) REFERENCES [Priorities]([Id]), 
    CONSTRAINT [FK_PendingTasks_ToStatus] FOREIGN KEY ([StatusId]) REFERENCES [Statuses]([Id]), 
    CONSTRAINT [FK_PendingTasks_ToTaskTypes] FOREIGN KEY ([TaskTypeId]) REFERENCES [TaskTypes]([Id]),
)
