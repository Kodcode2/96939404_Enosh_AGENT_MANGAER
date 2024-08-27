using Agents_Client.Services;
using Microsoft.AspNetCore.Mvc;

namespace Agents_Client.Controllers
{
    public class GeneralController(IGeneralService generalService) : Controller
    {

        //הצגת כל המשימות עם הצעה 
        public async Task<IActionResult> Index()
        {
            try
            {
            var missions = await generalService.GetAllMissionPorposal();
            return View(missions);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }

        }


        //הצגת דשבורד עם נתונים כללים על המערכת
        public async Task<IActionResult> DetailsDashBoard()
        {
            try
            {
            var dashboard = await generalService.GetGeneralDetails();
            return View(dashboard);

            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }


        //הצגת פרטי משימה לפי ID
        public async Task<IActionResult> Details(int id)
        {
            try
            {
            return View(await generalService.GetDetailsById(id));

            }
            catch (Exception ex) 
            {
                return RedirectToAction("Index");
            }
        }

        
        // כפתור להפוך משימה לפעילה
        public async Task<IActionResult> ToActive (int id)
        {
            try
            {
                await generalService.UpdateMissionToActive(id);
                return RedirectToAction("index");

            }
            catch (Exception ex)
            {

                return RedirectToAction("Index");
            }   
        } 

    }
}
