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
    }
}
