using Agents_Client.Models;
using Agents_Client.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Agents_Client.Services
{
    public class GeneralService(IHttpClientFactory clientFactory,IAgentService agentService,
        IMissionService missionService, ITargetService targetService): IGeneralService
    {
       public async Task<List<MissionActiveVM>> GetAllMissionPorposal()
        {
            var agents = await agentService.GetAllAgents();
            var targets = await targetService.GetAllTargets();
            var missions = await missionService.GetAllMissions();

            

            List<MissionActiveVM> missionproposal = new();
            
            var currentMission = missions.Where(m => m.MissionStatus == MissionVM.Status.proposal).ToList();
            
            foreach (var mission in currentMission)
            {
                var agent = agents.FirstOrDefault(a => a.Id == mission.AgentId);
                var target = targets.FirstOrDefault(t => t.Id == mission.TargetId);
                var distance = CalculateDistance(agent, target);
                
                missionproposal.Add(new MissionActiveVM
                {
                    Id = mission.Id,
                    NickName = agent.NickName,
                    Location_X_Agent = agent.Location_X,
                    Location_Y_Agent = agent.Location_Y,
                    TargetName = target.Name,
                    TargetPosition = target.Position,
                    Location_X_Target = target.Location_X,
                    Location_Y_Target = target.Location_Y,
                    TimeLeft = mission.TimeLeft,
                    Distance =distance,

                });
            }
            return missionproposal;
        }




        public double CalculateDistance(AgentVM agent, TargetVM target)
        {
            var distance = Math.Sqrt(Math.Pow(agent.Location_X - target.Location_X, 2)
                                + Math.Pow(agent.Location_Y - target.Location_Y, 2));
            return distance;

        }


        public async Task<GeneralVM> GetGeneralDetails()
        {
            var agents = await agentService.GetAllAgents();
            var targets = await targetService.GetAllTargets();
            var missions = await missionService.GetAllMissions();

            var numAgents = agents.Count;
            var numAgentsActive = agents.Where(a => a.AgentStatus == AgentVM.Status.Activate).Count();
            var numTargets = targets.Count;
            var numTargetsEliminated = targets.Where(t => t.TargetStatus == TargetVM.Status.eliminated).Count();
            var numMissions = missions.Count;
            var numMissionsActivate = missions.Where(m => m.MissionStatus == MissionVM.Status.OnMission).Count();
            var agentInTargets = agents.Count() / targets.Count();
            var agentsInProposal = agents.Where(a => a.AgentStatus == AgentVM.Status.dormant).Count() /
                targets.Where(t => t.TargetStatus == TargetVM.Status.live).Count();

            GeneralVM generalVM = new()
            {
                Agents = numAgents,
                AgentsActivate = numAgentsActive,
                Targets = numTargets,
                TargetsElimnated = numTargetsEliminated,
                Missions = numMissions,
                MissionsActivate = numMissionsActivate,
                AgentInTargets = agentInTargets,

            };
            return generalVM;

        }


        public async Task<MissionActiveVM> GetDetailsById(int id)
        {
             var missions = await GetAllMissionPorposal();
            var mission = missions.FirstOrDefault(m => m.Id == id) ??
                throw new Exception("Not Found");
            return mission;
        }


        public async Task<string> UpdateMissionToActive(int id)
        {
            var httpClient = clientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Put, $"http://localhost:5226/Missions/{id}");
            var httpContent = new StringContent(
                JsonSerializer.Serialize
                ("status : assigned"),
                Encoding.UTF8,
                "application/json"
              );
            request.Content = httpContent;
            var res = await httpClient.SendAsync(request);
            if (res.IsSuccessStatusCode)
            {
                return "Success";
            }
            return "Not Succsess";
        }


    }
}
