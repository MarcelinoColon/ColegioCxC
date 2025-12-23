using Aplicacion.Concepto.DTOs;
using Aplicacion.Estudiante.DTOs;

namespace Aplicacion.Cargo.DTOs
{
    public class CargoDto
    {
        public int? Id { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalCargo { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; }
        public int? CargoId { get; set; }
        public EstudianteDto? Estudiante { get; set; }
        public ConceptoDto? Concepto { get; set; }
    }
}
