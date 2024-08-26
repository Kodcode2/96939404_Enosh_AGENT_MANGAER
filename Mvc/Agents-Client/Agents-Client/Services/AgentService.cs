using Agents_Client.ViewModel;
using System.Text.Json;

namespace Agents_Client.Services
{
    public class AgentService(IHttpClientFactory clientFactory,IMissionService missionService ):IAgentService
    {
        private readonly string baseUrl = "http://localhost:5226/agents";
        public async Task<List<AgentVM>> GetAllAgents()
        {
            var httpClient = clientFactory.CreateClient();
            var result = await httpClient.GetAsync(baseUrl);
            if (result.IsSuccessStatusCode)
            {
                var content = await result.Content.ReadAsStringAsync();
                List<AgentVM>? agents = JsonSerializer.Deserialize<List<AgentVM>>
                    (content, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                return agents;
            }
            else
            {
                throw new Exception(await result.Content.ReadAsStringAsync());
            }
        }


        public async Task<List<AgentVM>> GetAgents()
        {
            var agents = await GetAllAgents();
            var missions = await missionService.GetAllMissions();


            List<AgentVM> agentLIst = new();
            foreach (var agent in agents)
            {
                
                double timeLeft = missions.FirstOrDefault(m => m.AgentId == agent.Id).TimeLeft
                    ?? 0;

                var activeMissions = missions.Where(m => m.AgentId == agent.Id && m.MissionStatus == MissionVM.Status.OnMission).ToList()
                    ?? [];

                var numOfKills = missions.Where(m => m.AgentId == agent.Id && m.MissionStatus == MissionVM.Status.MissionEnd).Count();

                agentLIst.Add(new AgentVM
                {
                    NickName = agent.NickName,
                    Image = agent.Image,
                    Location_X = agent.Location_X,
                    Location_Y = agent.Location_Y,
                    AgentStatus = agent.AgentStatus,
                    TimeLeft = timeLeft,
                    MissionActivate = activeMissions,
                    NumOfKills = numOfKills

                });
            }
            return agentLIst;
        }
    }
}
