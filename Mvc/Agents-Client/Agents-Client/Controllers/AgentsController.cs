using Agents_Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agents_Client.Controllers
{
    public class AgentsController(IAgentService agentService) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var agents = await agentService.GetAgents();
            return View(agents);
        }
    }
}
