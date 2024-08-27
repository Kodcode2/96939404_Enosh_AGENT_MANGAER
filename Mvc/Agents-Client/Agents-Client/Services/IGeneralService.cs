using Agents_Client.ViewModel;

namespace Agents_Client.Services
{
    public interface IGeneralService
    {
        Task<List<MissionActiveVM>> GetAllMissionPorposal();
        Task<GeneralVM> GetGeneralDetails();
        Task<MissionActiveVM> GetDetailsById(int id);
        Task<string> UpdateMissionToActive(int id);
    }
}
