using AgentRestApi.Dto;
using AgentRestApi.Models;

namespace AgentRestApi.Services
{
    public interface ITargetService
    {
        Task<TargetModel?> CreateTargetAsync(TargetDto targetDto);
        Task<List<TargetModel?>> GetAllTargetAsync();
        Task<TargetModel> PinTarget(LocationDto location, int id);
        Task<TargetModel> MoveTarget(DirectionDto direction, int id);
    }
}
