
namespace Dominio
{
    public class PagoEntity
    {
        public int? Id { get; private set; }
        public int EstudianteId { get; private set; }
        public DateTime FechaPago { get; private set; }
        public decimal MontoRecibido { get; private set; }
        public decimal MontoUsado { get; private set; }
        public decimal SaldoDisponible { get; private set; }


        public PagoEntity(int estudianteId, DateTime fechaPago, decimal montoRecibido, decimal montoUsado)
        {
            if (estudianteId <= 0)
                throw new ArgumentOutOfRangeException(nameof(estudianteId));
            if (fechaPago > DateTime.UtcNow)
                throw new ArgumentException("La fecha de generación no puede ser futura.", nameof(fechaPago));
            if (montoRecibido <= 0)
                throw new ArgumentException("El monto recibido no puede ser negativo.", nameof(montoRecibido));
            if (montoUsado < 0)
                throw new ArgumentException("El monto usado no puede ser negativo.", nameof(montoUsado));
            if(montoUsado > montoRecibido)
                throw new ArgumentException("El monto usado no puede ser mayor al monto recibido.", nameof(montoUsado));
           

            EstudianteId = estudianteId;
            FechaPago = fechaPago;
            MontoRecibido = montoRecibido;
            MontoUsado = montoUsado;
            SaldoDisponible = montoRecibido - montoUsado;
        }
        public PagoEntity(int id,int estudianteId, DateTime fechaPago, decimal montoRecibido, decimal montoUsado)
        {
            if(id <= 0)
                throw new ArgumentOutOfRangeException(nameof(id));
            if (estudianteId <= 0)
                throw new ArgumentOutOfRangeException(nameof(estudianteId));
            if (fechaPago > DateTime.UtcNow)
                throw new ArgumentException("La fecha de generación no puede ser futura.", nameof(fechaPago));
            if (montoRecibido <= 0)
                throw new ArgumentException("El monto recibido no puede ser negativo.", nameof(montoRecibido));
            if (montoUsado < 0)
                throw new ArgumentException("El monto usado no puede ser negativo.", nameof(montoUsado));
            if (montoUsado > montoRecibido)
                throw new ArgumentException("El monto usado no puede ser mayor al monto recibido.", nameof(montoUsado));


            Id = id;
            EstudianteId = estudianteId;
            FechaPago = fechaPago;
            MontoRecibido = montoRecibido;
            MontoUsado = montoUsado;
            SaldoDisponible = montoRecibido - montoUsado;
        }

        public void AplicarMonto(decimal monto)
        {
            if (monto <= 0)
                throw new ArgumentOutOfRangeException(nameof(monto),
                    "El monto a aplicar debe ser mayor que cero.");

            if (monto > SaldoDisponible)
                throw new InvalidOperationException(
                    "El monto a aplicar excede el saldo disponible.");

            MontoUsado += monto;
            SaldoDisponible -= monto;
        }

    }
}
