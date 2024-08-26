using Agents_Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agents_Client.Controllers
{
    public class MissionController(IMissionService missionService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var missions = await missionService.GetAllMissions();
            return View(missions);
        }
    }
}
