using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Pago
    {
        public int Id { get; set; }
        public int EstudianteId { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoRecibido { get; set; }
        public decimal MontoUsado { get; set; }
        public decimal SaldoDisponible { get; set; }
    }
}
