using Aplicacion.Interfaces.UseCase;
using Aplicacion.Pago.CasosDeUso;
using Aplicacion.Pago.DTOs;
using Dominio;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    [Route("Pago")]
    public class PagoController : Controller
    {
        private readonly ICreatePagoUseCase<PagoDto, PagoEntity> _registrarPagoNuevoUseCase;
        public PagoController(ICreatePagoUseCase<PagoDto, PagoEntity> registrarPagoNuevoUseCase)
        {
            _registrarPagoNuevoUseCase = registrarPagoNuevoUseCase;
        }
        [HttpPost("nuevo/{cargoId:int}")]
        public async Task<IActionResult> PagarCargo(PagoCargoVm model, int cargoId)
        {
            try
            {
                var dto = new PagoDto
                {
                    EstudianteId = model.EstudianteId,
                    MontoRecibido = model.MontoRecibido
                };

                await _registrarPagoNuevoUseCase.AddAsync(dto, cargoId);

                return RedirectToAction("Index", "Cargo");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;

                return RedirectToAction("Index", "Cargo");
                
            }

        }
    }
}
