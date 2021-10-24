using System.Collections.Generic;
using TaskAssistant.Domain.Entities;

namespace TaskAssistant.Api.Services.Interfaces
{
    public interface IPendingTaskService
    {
        List<PendingTask> GetAll();

        PendingTask Get(int id);

        long Add(PendingTask pendingTask);

        bool Update(PendingTask pendingTask);

        void Delete(int id);
    }
}