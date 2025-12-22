using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CargoRepository : ICreateRangeRepository<CargoEntity>
    {
        private readonly ColegioDbContext _context;
        public CargoRepository(ColegioDbContext context)
        {
            _context = context;
        }

        public async Task AddRange(IEnumerable<CargoEntity> entities)
        {
            var cargos = entities.Select(c => MapToModel(c));

            await _context.AddRangeAsync(cargos);

            await _context.SaveChangesAsync();
        }

        #region
        private static Cargo MapToModel(CargoEntity cargoEntity)
        {
            var cargo = new Cargo
            {
                CargoId = cargoEntity.CargoId,
                ConceptoId = cargoEntity.ConceptoId,
                EstudianteId = cargoEntity.EstudianteId,
                TotalCargo = cargoEntity.TotalCargo,
                TotalPagado = cargoEntity.TotalPagado,
                SaldoPendiente = cargoEntity.SaldoPendiente,
                Estado = cargoEntity.Estado,
                FechaGeneracion = cargoEntity.FechaGeneracion,
                FechaVencimiento = cargoEntity.FechaVencimiento
            };

            return cargo;
        }


        #endregion
    }
}
