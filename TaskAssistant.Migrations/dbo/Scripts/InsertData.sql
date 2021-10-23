Select *
from Plans

insert into Plans(Title, Description, PlanTypeId, UserId)
Values ('Test Plan', 'This is a test plan.', 1, 1);

Select *
from PendingTasks 

insert into PendingTasks(PlanId, Title, Description, TaskTypeId)
values (4, 'Test Pending Task', 'This is a test pending task.', 1);


Select *
from Statuses

Select *
from Priorities

Select *
from Users

Insert into Users (AuthorizationId, FirstName, LastName, Email)
Values ('abbe06d0-d8a6-4ac1-9d33-e5bf646f2dc2', 'Admin', 'User', 'admin@gmail.com')
