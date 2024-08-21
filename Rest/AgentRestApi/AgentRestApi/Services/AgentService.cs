
using AgentRestApi.Data;
using AgentRestApi.Dto;
using AgentRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AgentRestApi.Services
{
    public class AgentService(ApplicationDbContext context) : IAgentService
    {
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


    }
}
