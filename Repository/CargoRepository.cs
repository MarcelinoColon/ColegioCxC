using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class CargoRepository : ICreateRangeRepository<CargoEntity>, IReadRepository<CargoEntity>
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

        public async Task<CargoEntity> GetByIdAsync(int id)
        {
            var cargo = await _context.Cargos.FindAsync(id);

            if (cargo == null)
                throw new ArgumentNullException(nameof(cargo));

            return MapToEntity(cargo);
        }

        public async Task<IEnumerable<CargoEntity>> GetAllAsync()
        {
            var cargos = await _context.Cargos.Include(c => c.Estudiante)
                .Include(c => c.Concepto)
                .AsNoTracking()
                .ToListAsync();

            return cargos.Select(c => MapToEntity(c));
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

        private static CargoEntity MapToEntity(Cargo cargo)
        {
            var cargoEntity = new CargoEntity(cargo.Id, cargo.EstudianteId, cargo.ConceptoId,
                cargo.FechaGeneracion, cargo.TotalCargo, cargo.TotalPagado, cargo.SaldoPendiente, 
                cargo.Estado, cargo.CargoId);

            cargoEntity.SetEstudiante(new EstudianteEntity(cargo.Estudiante.Id, cargo.Estudiante.Nombre,
                 cargo.Estudiante.Apellido, cargo.Estudiante.Matricula));

            cargoEntity.SetConcepto(new ConceptoEntity(cargo.Concepto.Id, cargo.Concepto.Nombre,
                cargo.Concepto.Monto, cargo.Concepto.EsMora));

            return cargoEntity;
        }


        #endregion
    }
}
