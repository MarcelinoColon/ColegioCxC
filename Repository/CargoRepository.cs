using Aplicacion.Interfaces.Repository;
using Data;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class CargoRepository : ICreateRangeRepository<CargoEntity>, IReadRepository<CargoEntity>, IUpdateRepository<CargoEntity>
    {
        private readonly ColegioDbContext _context;
        public CargoRepository(ColegioDbContext context)
        {
            _context = context;
        }

        public async Task<(IEnumerable<CargoEntity> Items, int TotalRecords)> GetAllPaginatedAsync(int pageSize, int currentPage)
        {
            var query = _context.Cargos.Include(c => c.Estudiante)
                .Include(c => c.Concepto).OrderByDescending(c => c.FechaGeneracion).AsNoTracking();

            var totalRecords = await query.CountAsync();

            var itemsModel = await query.Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();

            var itemsEntity = itemsModel.Select(c => MapToEntity(c)).ToList();

            return (itemsEntity, totalRecords);
        }

        public async Task AddRange(IEnumerable<CargoEntity> entities)
        {
            var cargos = entities.Select(c => MapToModel(c));

            await _context.AddRangeAsync(cargos);

            await _context.SaveChangesAsync();
        }

        public async Task<CargoEntity> GetByIdAsync(int id)
        {
            var cargo = await _context.Cargos
                .Include(c => c.Estudiante)
                .Include(c => c.Concepto)
                .FirstOrDefaultAsync(c => c.Id == id);

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
        public async Task Update(CargoEntity entity)
        {
            // 1. Buscamos el MODELO de base de datos original (trackeado)
            var model = await _context.Cargos.FindAsync(entity.Id);

            if (model == null)
                throw new KeyNotFoundException($"No se encontró el cargo con Id {entity.Id} para actualizar.");

            // 2. Pasamos los valores ACTUALIZADOS de tu Entidad de Dominio al Modelo de BD
            // Solo actualizamos los campos que pueden cambiar en tu lógica de negocio
            model.TotalPagado = entity.TotalPagado;
            model.SaldoPendiente = entity.SaldoPendiente;
            model.Estado = entity.Estado;

            // No hace falta llamar a Update(), EF detecta los cambios en 'model'
            // El UnitOfWork.SaveChangesAsync() se encargará de persistirlo.
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
