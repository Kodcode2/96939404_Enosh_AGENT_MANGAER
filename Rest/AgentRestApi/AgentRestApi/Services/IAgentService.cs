using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Services
{
    public interface IAgentService
    {
        Task<AgentModel?> CreateAgentAsync(AgentDto agentDto);
        Task<List<AgentModel?>> GetAllAgentsAsync();
        Task<AgentModel?> GetAgentById(int id);
        Task<AgentModel?> UpdateAgentAsync(AgentDto agent, int id);
        Task<AgentModel> DeleteAgentAsync(int id);
    }
}
