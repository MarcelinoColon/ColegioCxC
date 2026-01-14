namespace Web.Models
{
    public class PagoCargoVm
    {
        public int CargoId { get; set; }
        public int EstudianteId { get; set; }

        public string EstudianteNombre { get; set; }
        public string Concepto { get; set; }
        public decimal SaldoDisponible { get; set; }
        public decimal MontoRecibido { get; set; }
    }

}
