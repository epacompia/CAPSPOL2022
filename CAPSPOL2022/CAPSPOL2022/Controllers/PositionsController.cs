using CAPSPOL2022.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace CAPSPOL2022.Controllers
{
    public class PositionsController:Controller
    {
        
        public PositionsController(IConfiguration configuration)
        {

        }



        public IActionResult Create()
        {
            
                return View();
        }


        [HttpPost]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View(position);
            }

            return View();
        }


    }
}
