
using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace AgentRestApi.Services
{
    public class AgentService(ApplicationDbContext context, IServiceProvider serviceProvider) : IAgentService
    {
        private IMissionService missionService = serviceProvider.GetRequiredService<IMissionService>();
        public async Task<AgentModel?> CreateAgentAsync(AgentDto agentDto)
        {
            if(agentDto == null)    
            {
                throw new Exception("Not Found");
            }
            AgentModel model = new()
            {
                NickName = agentDto.NickName,
                Image = agentDto.PhotoUrl
            };
            await context.AddAsync(model);
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<List<AgentModel?>> GetAllAgentsAsync()
        {
            return await context.Agents.ToListAsync();
        }

        public async Task<AgentModel?> GetAgentById(int id)
        {
            AgentModel agent = await context.Agents.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"This agent with {id} isn't exists");
            return agent;
        }
         
        
        public async Task<AgentModel?> UpdateAgentAsync(AgentDto agent, int id)
        {
            AgentModel model = await context.Agents.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"This agent with {id} isn't exists");
            model.NickName = agent.NickName;
            model.Image = agent.PhotoUrl;
            await context.SaveChangesAsync();
            return model;
        }

        public async Task<AgentModel> DeleteAgentAsync(int id)
        {
            var agent = await context.Agents.FirstOrDefaultAsync(a =>a.Id == id)
                ?? throw new Exception($"This agent with {id} isn't exists");
            context.Agents.Remove(agent);
            context.SaveChanges();
            return agent;
        }


        public async Task<AgentModel> PinAgent(LocationDto location, int id)
        {
            var model = await context.Agents.FirstOrDefaultAsync(a => a.Id == id)
                ?? throw new Exception($"This agent with {id} isn't exists");
            if (location == null ||
               location.X > 1000 || location.X < 0
               || location.Y > 1000 || location.Y < 0)
            {
                throw new Exception("No location");
            }

            model.Location_Y = location.Y;
            model.Location_X = location.X;
            await context.SaveChangesAsync();
            return model;
        }


       
        
        private readonly Dictionary<string, (int x, int y)> directions = new()
        {
            {"n",(0,1) },
            {"e",(1,0) },
            {"w",(-1,0) },
            {"s",(0,-1) },
            {"ne",(1,1) },
            {"nw",(-1,1) },
            {"se",(1,-1) },
            {"sw",(-1,-1) }
        };


       
        
        public async Task<AgentModel> MoveAgent(DirectionDto direction, int id)
        {
            var model = await context.Agents.FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new Exception($"This agent with {id} isn't exists");

            bool IsExist = directions.TryGetValue(direction.direction, out var result);
            if (!IsExist)
            {
                throw new Exception("Not exists");
            }
            if (model.AgentStatus == AgentModel.Status.Activate)
            {
                throw new Exception("Is activate");
            }

            model.Location_X += directions[direction.direction].x;
            model.Location_Y += directions[direction.direction].y;
            
            if (model.Location_X > 1000 || model.Location_X < 0
                || model.Location_Y > 1000 && model.Location_Y < 0)
            {
                throw new Exception("No Allowed to get to this place");
            }
            await missionService.ProposeMissionAsync(model.Id);
            await context.SaveChangesAsync();
            return model;

        }


    }
}
