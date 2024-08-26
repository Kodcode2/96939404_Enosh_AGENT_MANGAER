using Agents_Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agents_Client.Controllers
{
    public class TargetController(ITargetService targetService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var targets = await targetService.GetTargets();
            return View(targets);
        }
    }
}
