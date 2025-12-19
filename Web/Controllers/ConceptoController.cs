using Aplicacion.Concepto.DTOs;
using Aplicacion.Interfaces.UseCase;
using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("Concepto")]
    public class ConceptoController : Controller
    {
        private readonly IReadUseCase<ConceptoDto, ConceptoEntity> _obtenerConceptoUseCase;
        private readonly ICreateUseCase<ConceptoDto, ConceptoEntity> _agregarConceptoUseCase;
        public ConceptoController(IReadUseCase<ConceptoDto, ConceptoEntity> obtenerConceptoUseCase,
            ICreateUseCase<ConceptoDto, ConceptoEntity> agregarConceptoUseCase)
        {
            _obtenerConceptoUseCase = obtenerConceptoUseCase;
            _agregarConceptoUseCase = agregarConceptoUseCase;
        }

        public async Task<IActionResult> Index()
        {
            var conceptos = await _obtenerConceptoUseCase.GetAll();

            return View(conceptos);
        }

        [HttpGet("crear")]
        public async Task<IActionResult> Create()
        {
            try
            {
                return View("Add", new ConceptoDto());
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Ocurrió un error al intentar cargar el formulario: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost("crear")]
        public async Task<IActionResult> Create(ConceptoDto conceptoDto)
        {
            try
            {
                await _agregarConceptoUseCase.AddAsync(conceptoDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                return View("Add", conceptoDto);
            }
        }
    }
}
