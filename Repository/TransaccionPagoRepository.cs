using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class TransaccionPagoRepository : ICreateRepository<TransaccionPagoEntity>
    {
        private readonly ColegioDbContext _context;
        public TransaccionPagoRepository(ColegioDbContext context)
        {
            _context = context;
        }

        public async Task Create(TransaccionPagoEntity entity)
        {
            var model = MapToModel(entity);
            await _context.TransaccionPagos.AddAsync(model);
        }

        #region Mappers
        private static Data.TransaccionPago MapToModel(TransaccionPagoEntity entity)
        {
            return new Data.TransaccionPago
            {
                PagoId = entity.PagoId,
                CargoId = entity.CargoId,
                MontoAplicado = entity.MontoAplicado,
                FechaRegistro = entity.FechaRegistro
            };
        }
        #endregion
    }
}
