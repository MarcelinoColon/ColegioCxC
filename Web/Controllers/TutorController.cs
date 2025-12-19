using Aplicacion.Interfaces.UseCase;
using Aplicacion.Tutor.DTOs;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("Tutor")]
    public class TutorController : Controller
    {
        private readonly IReadUseCase<TutorDto, TutorEntity> _obtenerTutorUseCase;
        private readonly IUpsertUseCase<TutorDto, TutorEntity> _agregarActualizarTutorUseCase;
        private readonly ISearchUseCase<TutorDto, TutorEntity> _buscarTutorUseCase;

        public TutorController(IReadUseCase<TutorDto, TutorEntity> obtenerTutorUseCase, 
            IUpsertUseCase<TutorDto, TutorEntity> agregarActualizarTutorUseCase,
            ISearchUseCase<TutorDto, TutorEntity> buscarTutorUseCase)
        {
            _obtenerTutorUseCase = obtenerTutorUseCase;
            _agregarActualizarTutorUseCase = agregarActualizarTutorUseCase;
            _buscarTutorUseCase = buscarTutorUseCase;
        }
        public async Task<IActionResult> Index()
        {
            var tutoresDtos = await _obtenerTutorUseCase.GetAll();

            return View(tutoresDtos);
        }

        [HttpGet("upsert")]
        [HttpGet("upsert/{id:int}")]
        public async Task<IActionResult> Upsert(int? id)
        {
            try
            {
                if (id is null or 0)
                {
                    return View("AddOrUpdate", new TutorDto());
                }

                var tutorDto = await _obtenerTutorUseCase.GetById((int)id);

                if (tutorDto is null)
                {
                    TempData["Error"] = "El tutor solicitado no existe.";
                    return RedirectToAction("Index");
                }

                return View("AddOrUpdate", tutorDto);
            }catch(Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al intentar cargar el formulario: {ex.Message}";

                return RedirectToAction("Index");
            }

        }

        [HttpPost("upsert/{id:int?}")]
        public async Task<IActionResult> Upsert(TutorDto dto)
        {
            try
            {
                await _agregarActualizarTutorUseCase.UpsertAsync(dto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View("AddOrUpdate", dto);
            }
        }

        [HttpGet("Buscar")]
        public async Task<IActionResult> BuscarTutores(string term)
        {
            var tutores = await _buscarTutorUseCase.SearchAsync(term);

            var resultado = tutores.Select(t =>
            {
                string nombreCompleto = $"{t.Nombre} {t.Apellido}".Trim();
                string parteCedula = string.IsNullOrWhiteSpace(t.Cedula)
                                     ? ""
                                     : $" - {t.Cedula}";
                return new
                {
                    id = t.Id,
                    text = $"{nombreCompleto}{parteCedula}"
                };
            });

            return Json(new { results = resultado });
        }
    }
}
