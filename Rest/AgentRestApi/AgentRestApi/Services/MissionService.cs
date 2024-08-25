using AgentRestApi.Data;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace AgentRestApi.Services
{
    public class MissionService(ApplicationDbContext context) : IMissionService
    {
       
        
        public async Task<List<MissionModel>> GetAllMissionsAsync()
        {
            return await context.Missions.ToListAsync();
        }

        public double CalculateDistance(AgentModel agent, TargetModel target)
        {
            var distance =  Math.Sqrt(Math.Pow(agent.Location_X - target.Location_X, 2)
                                + Math.Pow(agent.Location_Y - target.Location_Y, 2));
            return distance;
           
        }



        public async Task<List<TargetModel>> RelevantTargetPorpose()
        {
            var targets = await context.Targets
                .Where(a => a.TargetStatus == TargetModel.Status.live).ToListAsync()
                    ?? throw new Exception();
            return targets;
        }

           

        public async Task<bool> IsAgentValid(int id)
        {
            var agent = await context.Agents.FindAsync(id);
            if( agent.AgentStatus == AgentModel.Status.dormant)
            {
                return false;
            }
            return true;
        }



        public async Task<List<MissionModel>> ProposeMissionAsync(int id)
        {
            var agent = await context.Agents.FindAsync(id)
                ?? throw new Exception("Not found");
                
            var targets = await RelevantTargetPorpose();

            var targetsInDistance = targets
                .Where(t => (CalculateDistance(agent,t) < 200))
                .Select(t => t)
                .ToList();

            List<MissionModel> missions = new();
            
            if (!await IsAgentValid(agent.Id))
            {
                foreach (var target in targetsInDistance)
                {

                    MissionModel model = new()
                    {
                        AgentId = agent.Id,
                        AgentModel = agent,
                        TargetId = target.Id,
                        TargetModel = target,
                        Status = Status.proposal
                    };
                    missions.Add(model);
                }

                await context.AddRangeAsync(missions);
                await context.SaveChangesAsync();
                return missions;
            }
            throw new Exception("No avalible to Mission");
        }

        public bool  CheckIfCanChangeStatus(MissionModel mission, AgentModel agent, TargetModel target)
        {
            if(mission.Status == Status.proposal 
                && agent.AgentStatus == AgentModel.Status.dormant
                && target.TargetStatus == TargetModel.Status.live
                && CalculateDistance(agent,target) <= 200
                )
            {
                return true;
            }
            return false;
        }

        public async Task<MissionModel> EditMissionStatus(int id)
        {
            var mission = await context.Missions.FindAsync(id)
                ?? throw new Exception("No mission found");

            var agent = await context.Agents.FindAsync(mission.AgentId);
            var target = await context.Targets.FindAsync(mission.TargetId);

            if(CheckIfCanChangeStatus(mission,agent,target))
            {
                mission.Status = Status.OnMission;
                agent.AgentStatus = AgentModel.Status.Activate;
                target.TargetStatus = TargetModel.Status.hunted;
                mission.StartTime = DateTime.Now;
                await context.SaveChangesAsync();
                return mission;

            }
            throw new Exception("Don't allow to update");

        } 

        public bool CheckIfEqualsInMatrix(AgentModel agent, TargetModel target)
        {
            if(agent.Location_X == target.Location_X && agent.Location_Y == agent.Location_Y)
            {
                return false;
            }
            return true;
        }


        public AgentModel MoveAgent(AgentModel agent, TargetModel target)
        {
            if (CheckIfEqualsInMatrix(agent, target))
            {
                agent.Location_X = agent.Location_X < target.Location_X ? agent.Location_X += 1 : agent.Location_X;
                agent.Location_X = agent.Location_X > target.Location_X ? agent.Location_X -= 1 : agent.Location_X;
                agent.Location_Y = agent.Location_Y < target.Location_Y ? agent.Location_Y += 1 : agent.Location_Y;
                agent.Location_Y = agent.Location_Y > target.Location_Y ? agent.Location_Y -= 1 : agent.Location_Y;
            }
            return agent;
        }


        public async Task<List<MissionModel>> UpdateMission()
        {
            var missions = await context.Missions.Where(m => m.Status == Status.OnMission).ToListAsync();
            
           
            foreach (var mission in missions)
            {
                var agent = await context.Agents.FindAsync(mission.AgentId);
                var target = await context.Targets.FindAsync(mission.TargetId);
                var distance = CalculateDistance(agent, target);
                if (!CheckIfEqualsInMatrix(agent, target))
                {
                    target.TargetStatus = TargetModel.Status.eliminated;
                    agent.AgentStatus = AgentModel.Status.dormant;
                    mission.ExecuteTime = $"{DateTime.Now - mission.StartTime:mm\\:ss}";
                    mission.Status = Status.MissionEnd;
                    await context.SaveChangesAsync();
                }
                else
                {
                    mission.TimeLeft = distance / 5;
                    await context.SaveChangesAsync();
                    MoveAgent(agent, target);   
                }
            }

            return missions;



        }



            


            
                
            



        }
    }
