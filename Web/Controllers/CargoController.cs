using Aplicacion.Cargo.DTOs;
using Aplicacion.Concepto.DTOs;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Paginacion;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Threading.Tasks;
using Web.Models;

namespace Web.Controllers
{
    [Route("Cargo")]
    public class CargoController : Controller
    {
        private readonly IReadUseCase<CargoDto, CargoEntity> _obtenerCargoUseCase;
        private readonly ICreateUseCase<CargoInsertDto, CargoEntity> _crearCargoUseCase;

        public CargoController(IReadUseCase<CargoDto, CargoEntity> obtenerCargoUseCase, 
            ICreateUseCase<CargoInsertDto, CargoEntity> crearCargoUseCase)
        {
            _obtenerCargoUseCase = obtenerCargoUseCase;
            _crearCargoUseCase = crearCargoUseCase;
        }
        [HttpGet("")]
        [HttpGet("{page:int}/{size:int}")]
        public async Task<IActionResult> Index(int page, int size)
        {
            try
            {
                var cargosDtos = await _obtenerCargoUseCase.GetAllPaginated(size, page);

                return View(new CargoIndexVm()
                {
                    Pagination = cargosDtos
                });
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                var vmVacio = new CargoIndexVm
                {
                    Pagination = new PaginationDto<CargoDto>
                    {
                        Items = new List<CargoDto>(),
                        CurrentPage = 1,
                        PageSize = size,
                        TotalItems = 0
                    }
                };

                return View(vmVacio);
            }
        }

        [HttpGet("crear")]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View("Add", new CargoInsertDto());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al intentar cargar el formulario: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost("crear")]
        public async Task<IActionResult> Create(CargoInsertDto dto)
        {
            try
            {
                await _crearCargoUseCase.AddAsync(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View("Add", dto);
            }
        }
    }
}
