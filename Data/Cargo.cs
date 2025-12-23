using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Cargo
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public int ConceptoId { get; set; }
        public DateTime FechaGeneracion { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public decimal TotalCargo { get; set; }
        public decimal TotalPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
        public string Estado { get; set; }
        public int? CargoId { get; set; }
        public virtual Estudiante Estudiante { get; set; } = null!;
        public virtual Concepto Concepto { get; set; } = null!;

    }
}
