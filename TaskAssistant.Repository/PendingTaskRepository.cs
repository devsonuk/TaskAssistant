using Microsoft.Extensions.Options;
using TaskAssistant.Domain.Configuration;
using TaskAssistant.Domain.Entities;
using TaskAssistant.Repository.Interfaces;

namespace TaskAssistant.Repository
{
    public class PendingTaskRepository : GenericRepository<PendingTask>, IPendingTaskRepository
    {
        private readonly IOptions<AppSettings> _appSettings;

        public PendingTaskRepository(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings;
            ConnectionString = _appSettings.Value.ConnectionStrings.TaskAssistant;
        }
    }
}
