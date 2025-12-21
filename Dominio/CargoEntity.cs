using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class CargoEntity
    {
        public int? Id { get; private set; }
        public int EstudianteId { get;private set; }
        public int ConceptoId { get;private set; }
        public DateTime FechaGeneracion { get; private set; }
        public DateTime FechaVencimiento { get; private set; }
        public decimal TotalCargo { get; private set; }
        public decimal TotalPagado { get; private set; }
        public decimal SaldoPendiente { get; private set; }
        public string Estado { get; private set; }
        public int? CargoId { get; private set; }

        public CargoEntity(int id, int estudianteId, int conceptoId, DateTime fechaGeneracion, DateTime fechaVencimiento,
                           decimal totalCargo, decimal totalPagado, decimal saldoPendiente, string estado, int? cargoId)
        {
            if (id <= 0) 
                throw new ArgumentException("El Id debe ser positivo.", nameof(id));
            if (estudianteId <= 0) 
                throw new ArgumentException("El EstudianteId debe ser positivo.", nameof(estudianteId));
            if (conceptoId <= 0) 
                throw new ArgumentException("El ConceptoId debe ser positivo.", nameof(conceptoId));
            if (cargoId.HasValue && cargoId.Value <= 0) 
                throw new ArgumentException("El CargoId padre debe ser positivo.", nameof(cargoId));


            if (fechaGeneracion > DateTime.Now)
                throw new ArgumentException("La fecha de generación no puede ser futura.", nameof(fechaGeneracion));

            if (fechaVencimiento < fechaGeneracion.Date)
                throw new ArgumentException("La fecha de vencimiento no puede ser anterior a la fecha de generación.", nameof(fechaVencimiento));

            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado es obligatorio.", nameof(estado));

            if (totalCargo < 0) 
                throw new ArgumentException("El total no puede ser negativo.", nameof(totalCargo));
            if (totalPagado < 0) 
                throw new ArgumentException("El pagado no puede ser negativo.", nameof(totalPagado));
            if (saldoPendiente < 0) 
                throw new ArgumentException("El saldo no puede ser negativo.", nameof(saldoPendiente));

            if (totalCargo != totalPagado + saldoPendiente)
                throw new InvalidOperationException($"Inconsistencia matemática: Cargo ({totalCargo}) != Pagado ({totalPagado}) + Pendiente ({saldoPendiente}).");

            if (saldoPendiente == 0 && estado == "Pendiente")
                throw new ArgumentException("Incoherencia: Saldo es 0 pero estado dice Pendiente.", nameof(estado));

            Id = id;
            EstudianteId = estudianteId;
            ConceptoId = conceptoId;
            FechaGeneracion = fechaGeneracion;
            FechaVencimiento = fechaVencimiento;
            TotalCargo = totalCargo;
            TotalPagado = totalPagado;
            SaldoPendiente = saldoPendiente;
            Estado = estado;
            CargoId = cargoId;
        }

        public CargoEntity(int estudianteId, int conceptoId, DateTime fechaGeneracion, DateTime fechaVencimiento,
                           decimal totalCargo, decimal totalPagado, decimal saldoPendiente, string estado, int? cargoId)
        {
            if (estudianteId <= 0)
                throw new ArgumentException("El EstudianteId debe ser positivo.", nameof(estudianteId));
            if (conceptoId <= 0)
                throw new ArgumentException("El ConceptoId debe ser positivo.", nameof(conceptoId));
            if (cargoId.HasValue && cargoId.Value <= 0)
                throw new ArgumentException("El CargoId padre debe ser positivo.", nameof(cargoId));


            if (fechaGeneracion > DateTime.Now)
                throw new ArgumentException("La fecha de generación no puede ser futura.", nameof(fechaGeneracion));

            if (fechaVencimiento < fechaGeneracion.Date)
                throw new ArgumentException("La fecha de vencimiento no puede ser anterior a la fecha de generación.", nameof(fechaVencimiento));

            if (string.IsNullOrWhiteSpace(estado))
                throw new ArgumentException("El estado es obligatorio.", nameof(estado));

            if (totalCargo < 0)
                throw new ArgumentException("El total no puede ser negativo.", nameof(totalCargo));
            if (totalPagado < 0)
                throw new ArgumentException("El pagado no puede ser negativo.", nameof(totalPagado));
            if (saldoPendiente < 0)
                throw new ArgumentException("El saldo no puede ser negativo.", nameof(saldoPendiente));

            if (totalCargo != totalPagado + saldoPendiente)
                throw new InvalidOperationException($"Inconsistencia matemática: Cargo ({totalCargo}) != Pagado ({totalPagado}) + Pendiente ({saldoPendiente}).");

            if (saldoPendiente == 0 && estado == "Pendiente")
                throw new ArgumentException("Incoherencia: Saldo es 0 pero estado dice Pendiente.", nameof(estado));

            EstudianteId = estudianteId;
            ConceptoId = conceptoId;
            FechaGeneracion = fechaGeneracion;
            FechaVencimiento = fechaVencimiento;
            TotalCargo = totalCargo;
            TotalPagado = totalPagado;
            SaldoPendiente = saldoPendiente;
            Estado = estado;
            CargoId = cargoId;
        }

        public void ActualizarPago(decimal montoPagado)
        {
            if (montoPagado <= 0)
                throw new ArgumentException("El monto pagado debe ser positivo.", nameof(montoPagado));
            if (montoPagado > SaldoPendiente)
                throw new InvalidOperationException("El monto pagado no puede exceder el saldo pendiente.");


            TotalPagado += montoPagado;
            SaldoPendiente -= montoPagado;

            if(SaldoPendiente == 0)
            {
                Estado = "Pagado";
            }
            else
            {
                Estado = "Pago parcial";
            }
        }
    }
}
