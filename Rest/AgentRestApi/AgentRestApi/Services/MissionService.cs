using AgentRestApi.Data;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Services
{
    public class MissionService(ApplicationDbContext context) : IMissionService
    {
        public async Task<List<MissionModel>> GetAllMissionsAsync()
        {
            return await context.Missions.ToListAsync();
        }

        public double CalculateDistance(int XAgent,int XTarget,int YAgent,int YTarget )
        {
            var distance =  Math.Sqrt(Math.Pow(XAgent - XTarget, 2)
                                + Math.Pow(YAgent - YTarget, 2));
            return distance;
           
        }

        public async Task<AgentModel> FindAgentById(int id) =>
            await context.Agents.FirstOrDefaultAsync(x => x.Id == id)
               ?? throw new Exception($"Agent wirh this {id} not found");


        public async Task<TargetModel> FindTargetById(int id) =>
            await context.Targets.FirstOrDefaultAsync(x => x.Id == id)
                  ?? throw new Exception($"Target with this {id} not found");

        public async Task<bool> IsAgentIsValid(int id)
        {
            var agent = await FindAgentById(id);
            if (agent.AgentStatus == AgentModel.Status.Activate)
            {
                return false;
            }
            return true;
        }
            

        public async Task<bool> IsTargetValid(int id)
        {
            var target = await FindTargetById(id);
            if( target.TargetStatus == TargetModel.Status.eliminated)
            {
                return false;
            }
            return true;
        }



        public async Task<MissionModel> CreateMissionAsync(int idAgent, int idTarget)
        {
            var agent = await FindAgentById(idAgent);
                

            var target = await FindTargetById(idTarget);


            if (!await IsAgentIsValid(idAgent) && !await IsTargetValid(idTarget))
            {



                var distance = CalculateDistance(agent.Location_X, target.Location_X, agent.Location_Y, target.Location_Y);

                var timeLeft = distance / 5;

                if (distance > 200)
                {
                    throw new Exception("You can't create a mission");
                }

                MissionModel model = new()
                {
                    AgentId = idAgent,
                    AgentModel = agent,
                    TargetId = idTarget,
                    TargetModel = target,
                    TimeLeft = timeLeft,
                    Status = Status.proposal
                };

                await context.AddAsync(model);
                await context.SaveChangesAsync();
                return model;
            }
            throw new Exception("No avalible to Mission");
        }


        public async Task<> MoveAgent(int missionId)
        {
            var mission = await context.Missions.FindAsync(missionId);
              ?? throw new Exception("Mission not found");
            ta
        }


        }
    }
