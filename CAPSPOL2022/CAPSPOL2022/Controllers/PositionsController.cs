using CAPSPOL2022.Models;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using static CAPSPOL2022.Servicios.RepositoryPositions;

namespace CAPSPOL2022.Controllers
{
    public class PositionsController:Controller
    {
        private readonly IRepositoryPositionsService repositoryPositionsService;

        public PositionsController(IRepositoryPositionsService repositoryPositionsService)
        {
            this.repositoryPositionsService = repositoryPositionsService;
        }

        //METODO PARA LISTAR POSICIONES INDEX
        public async Task<IActionResult> Index()
        {
            var listPosition =await repositoryPositionsService.ListPosition(1);
            return View(listPosition);
        }

        public IActionResult Create()
        {
            
                return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Position position)
        {
            if (!ModelState.IsValid)
            {
                return View(position);
            }
            //VALIDANDO SI EXISTE EL NOMBRE QUE SE GUARDARA
            var existNamePosition = await repositoryPositionsService.Exist(position.Name);
            if (existNamePosition)
            {
                ModelState.AddModelError(nameof(position.Name),$"El nombre {position.Name} ya existe en la base de datos");
                return View(position);
            }

            await repositoryPositionsService.Create(position);
            return RedirectToAction("Index");
        }


        //CREANDO EL METODO EDITAR REGISTRO 
        public async Task<IActionResult> Edit(int id)
        {
            var position=await repositoryPositionsService.GetForId(id);
            if (position is null)
            {
                return RedirectToAction("No se encontro el registro","Home");
            }
            return View(position);
        }


    }
}
