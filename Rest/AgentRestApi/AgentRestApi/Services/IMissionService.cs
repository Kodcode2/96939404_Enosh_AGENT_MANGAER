using AgentRestApi.Models;

namespace AgentRestApi.Services
{
    public interface IMissionService
    {
        Task<List<MissionModel>> GetAllMissionsAsync();
        Task<List<MissionModel>> UpdateMission();
        Task<MissionModel> EditMissionStatus(int id);
        Task<List<MissionModel>> ProposeMissionAsync(int id);

    }
}
