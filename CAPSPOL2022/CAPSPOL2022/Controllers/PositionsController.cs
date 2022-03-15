using Microsoft.AspNetCore.Mvc;
namespace CAPSPOL2022.Controllers
{
    public class PositionsController:Controller
    {
        public IActionResult Create()
        {
            return View();
        }
    }
}
