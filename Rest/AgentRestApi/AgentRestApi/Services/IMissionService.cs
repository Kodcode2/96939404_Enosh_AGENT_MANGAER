using AgentRestApi.Models;

namespace AgentRestApi.Services
{
    public interface IMissionService
    {
        Task<List<MissionModel>> GetAllMissionsAsync();
    }
}
