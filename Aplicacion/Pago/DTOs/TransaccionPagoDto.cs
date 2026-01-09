using Aplicacion.Cargo.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pago.DTOs
{
    public class TransaccionPagoDto
    {
        public int Id { get; set; }
        public int PagoId { get; set; }
        public int CargoId { get; set; }
        public decimal MontoAplicado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public CargoDto? Cargo { get; set; }
        public PagoDto? Pago { get; set; }
    }
}
