using System.Collections.Generic;
using System.Linq;
using TaskAssistant.Api.Services.Interfaces;
using TaskAssistant.Domain.Entities;
using TaskAssistant.Repository.Interfaces;

namespace TaskAssistant.Api.Services
{
    public class PendingTaskService : IPendingTaskService
    {
        private readonly IPendingTaskRepository _pendingTaskRepository;

        public PendingTaskService(IPendingTaskRepository pendingTaskRepository)
        {
            _pendingTaskRepository = pendingTaskRepository;
        }

        public List<PendingTask> GetPendingTasks()
        {
            return _pendingTaskRepository.GetAll().ToList();
        }
    }
}
