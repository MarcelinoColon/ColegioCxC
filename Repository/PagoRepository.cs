using Aplicacion.Interfaces.Repository;
using Data; // Tus modelos de BD
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Reflection; // Necesario para el truco del ID privado

namespace Repository
{
    public class PagoRepository : ICreateRepository<PagoEntity>
    {
        private readonly ColegioDbContext _context;

        public PagoRepository(ColegioDbContext context)
        {
            _context = context;
        }

        public async Task Create(PagoEntity entity) // O AddAsync segun tu interfaz
        {
            // 1. Mapear de Dominio a Datos
            var model = MapToModel(entity);

            // 2. Agregar al contexto
            await _context.Pagos.AddAsync(model);

            // 3. Guardar cambios AHORA MISMO para que la BD genere el ID
            // Nota: Esto se salta el UnitOfWork momentáneamente, pero es necesario
            // si necesitas el ID inmediatamente para la siguiente operación.
            await _context.SaveChangesAsync();

            // 4. TRUCO DE REFLECTION:
            // Como entity.Id tiene "private set", usamos Reflection para asignarle
            // el valor que la base de datos acaba de generar en 'model.Id'.
            typeof(PagoEntity)
                .GetProperty(nameof(PagoEntity.Id))
                ?.SetValue(entity, model.Id);
        }

        #region Mappers
        private static Data.Pago MapToModel(PagoEntity entity)
        {
            return new Data.Pago
            {
                // No mapeamos Id porque es nuevo (0 o null)
                EstudianteId = entity.EstudianteId,
                FechaPago = entity.FechaPago,
                MontoRecibido = entity.MontoRecibido,
                MontoUsado = entity.MontoUsado,
                SaldoDisponible = entity.SaldoDisponible
            };
        }
        #endregion
    }
}