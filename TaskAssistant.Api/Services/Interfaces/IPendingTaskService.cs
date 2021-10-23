using System.Collections.Generic;
using TaskAssistant.Domain.Entities;

namespace TaskAssistant.Api.Services.Interfaces
{
    public interface IPendingTaskService
    {
        List<PendingTask> GetPendingTasks();
    }
}