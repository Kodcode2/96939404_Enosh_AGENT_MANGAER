using Agents_Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agents_Client.Controllers
{
    public class GeneralController(IGeneralService generalService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var missions = await generalService.GetAllMissionPorposal();
            return View(missions);
        }

        public async Task<IActionResult> DetailsDashBoard()
        {
            var dashboard = await generalService.GetGeneralDetails();
            return View(dashboard);
        }
    }
}
