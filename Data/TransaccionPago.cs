using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class TransaccionPago
    {
        public int Id { get; set; }
        public int PagoId { get; set; }
        public int CargoId { get; set; }
        public decimal MontoAplicado { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
