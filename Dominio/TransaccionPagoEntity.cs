using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio
{
    public class TransaccionPagoEntity
    {
        public int? Id { get; private set; }
        public int PagoId { get; private set; }
        public int CargoId { get; private set; }
        public decimal MontoAplicado { get; private set; }
        public DateTime FechaRegistro { get; private set; }
        public PagoEntity? Pago { get; private set; }
        public CargoEntity? Cargo { get; private set; }


        public TransaccionPagoEntity(int pagoId, int cargoId, decimal montoAplicado, DateTime fechaRegistro)
        {
            if(pagoId <= 0)
                throw new ArgumentOutOfRangeException(nameof(pagoId));
            if(cargoId <= 0)
                throw new ArgumentOutOfRangeException(nameof(cargoId));
            if(montoAplicado <= 0)
                throw new ArgumentOutOfRangeException(nameof(montoAplicado));
            if(FechaRegistro > DateTime.UtcNow)
                throw new ArgumentException("La fecha de registro no puede ser futura.", nameof(fechaRegistro));

            PagoId = pagoId;
            CargoId = cargoId;
            MontoAplicado = montoAplicado;
            FechaRegistro = fechaRegistro;
        }
        public TransaccionPagoEntity(int id, int pagoId, int cargoId, decimal montoAplicado, DateTime fechaRegistro)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (pagoId <= 0)
                throw new ArgumentOutOfRangeException(nameof(pagoId));
            if (cargoId <= 0)
                throw new ArgumentOutOfRangeException(nameof(cargoId));
            if (montoAplicado <= 0)
                throw new ArgumentOutOfRangeException(nameof(montoAplicado));
            if (FechaRegistro > DateTime.UtcNow)
                throw new ArgumentException("La fecha de registro no puede ser futura.", nameof(fechaRegistro));

            Id = id;
            PagoId = pagoId;
            CargoId = cargoId;
            MontoAplicado = montoAplicado;
            FechaRegistro = fechaRegistro;
        }

        private void SetPago(PagoEntity pago)
        {
            if(pago == null)
                throw new ArgumentNullException(nameof(pago));
            Pago = pago;
        }
    }
}
