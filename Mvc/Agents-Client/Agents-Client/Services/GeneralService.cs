using Agents_Client.ViewModel;

namespace Agents_Client.Services
{
    public class GeneralService(IHttpClientFactory clientFactory)
    {
        private static IAgentService _agentService;
        private static IMissionService _missionService;

        public async Task<List<MissionVM>> GetAllMissionAssigned()
        {

        }
    }
}
