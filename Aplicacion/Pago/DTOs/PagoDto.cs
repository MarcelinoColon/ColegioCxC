using Aplicacion.Estudiante.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pago.DTOs
{
    public class PagoDto
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoRecibido { get; set; }
        public decimal SaldoDisponible { get; set; }
        public EstudianteDto Estudiante { get; set; }
    }
}
