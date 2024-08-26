using Agents_Client.ViewModel;

namespace Agents_Client.Services
{
    public interface IAgentService
    {
        Task<List<AgentVM>> GetAllAgents();
        Task<List<AgentVM>> GetAgents();
    }
}
