using Agents_Client.ViewModel;

namespace Agents_Client.Services
{
    public interface IMissionService
    {
        Task<List<MissionVM>> GetAllMissionsPorpose();
    }
}
