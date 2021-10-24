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

        /// <summary>
        /// Get all PendingTaks.
        /// </summary>
        /// <returns> List of PendingTasks</returns>
        public List<PendingTask> GetAll()
        {
            return _pendingTaskRepository.GetAll().ToList();
        }

        public PendingTask Get(int id)
        {
            return _pendingTaskRepository.Get(id);
        }

        public long Add(PendingTask pendingTask)
        {
            return _pendingTaskRepository.Add(pendingTask);
        }

        public bool Update(PendingTask pendingTask)
        {
            return _pendingTaskRepository.Update(pendingTask);
        }

        public void Delete(int id)
        {
            _pendingTaskRepository.Delete(id);
        }
    }
}
