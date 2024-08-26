using Agents_Client.ViewModel;

namespace Agents_Client.Services
{
    public interface ITargetService
    {
        Task<List<TargetVM>> GetAllTargets();
        Task<List<TargetVM>> GetTargets();
    }
}
