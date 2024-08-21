using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Services
{
    public interface ITargetService
    {
        Task<TargetModel?> CreateTargetAsync(TargetDto targetDto);
        Task<List<TargetModel?>> GetAllTargetAsync();
    }
}
