using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Tutor.CasosDeUso;
using Aplicacion.Tutor.DTOs;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Web.Controllers
{
    [Route("Estudiante")]
    public class EstudianteController : Controller
    {
        private readonly IReadUseCase<EstudianteDto, EstudianteEntity> _obtenerEstudianteUseCase;
        private readonly IUpsertUseCase<EstudianteDto, EstudianteEntity> _agregarActualizarEstudianteUseCase;

        public EstudianteController(
            IReadUseCase<EstudianteDto, EstudianteEntity> obtenerEstudianteUseCase,
            IUpsertUseCase<EstudianteDto, EstudianteEntity> agregarActualizarEstudianteUseCase)
        {
            _obtenerEstudianteUseCase = obtenerEstudianteUseCase;
            _agregarActualizarEstudianteUseCase = agregarActualizarEstudianteUseCase;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var estudiantesDto = await _obtenerEstudianteUseCase.GetAll();

            return View(estudiantesDto);
        }

        [HttpGet("upsert")]
        [HttpGet("upsert/{id:int}")]
        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                if (id is null or 0)
                {
                    return View("AddOrUpdate", new EstudianteDto());
                }

                var estudianteDto = await _obtenerEstudianteUseCase.GetById((int)id);

                if (estudianteDto is null)
                {
                    TempData["Error"] = "El estudiante solicitado no existe.";
                    return RedirectToAction("Index");
                }

                return View("AddOrUpdate", estudianteDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al intentar cargar el formulario: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost("upsert/{id:int?}")]
        public async Task<IActionResult> Upsert(EstudianteDto dto)
        {
            try
            {
                await _agregarActualizarEstudianteUseCase.UpsertAsync(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View("AddOrUpdate", dto);
            }
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            try
            {
                var estudianteDto = await _obtenerEstudianteUseCase.GetById(id);

                return View("Details", estudianteDto);
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al intentar cargar el formulario: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
