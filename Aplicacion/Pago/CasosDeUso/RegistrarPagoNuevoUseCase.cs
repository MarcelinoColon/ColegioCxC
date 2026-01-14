using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Pago.DTOs;
using Dominio;

namespace Aplicacion.Pago.CasosDeUso
{
    public class RegistrarPagoNuevoUseCase : ICreatePagoUseCase<PagoDto, PagoEntity>
    {
        private readonly IReadRepository<CargoEntity> _readCargoRepository;
        private readonly IUpdateRepository<CargoEntity> _updateCargoRepository;
        private readonly ICreateRepository<PagoEntity> _createPagoRepository;
        private readonly ICreateRepository<TransaccionPagoEntity> _createTransaccionPagoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RegistrarPagoNuevoUseCase(
            IReadRepository<CargoEntity> readCargoRepository,
            IUpdateRepository<CargoEntity> updateCargoRepository,
            ICreateRepository<PagoEntity> createPagoRepository,
            ICreateRepository<TransaccionPagoEntity> createTransaccionPagoRepository,
            IUnitOfWork unitOfWork)
        {
            _readCargoRepository = readCargoRepository;
            _updateCargoRepository = updateCargoRepository;
            _createPagoRepository = createPagoRepository;
            _createTransaccionPagoRepository = createTransaccionPagoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(PagoDto dto, int cargoId)
        {
            // 1. Validaciones iniciales
            if (dto == null) throw new ArgumentNullException(nameof(dto));
            if (cargoId <= 0) throw new ArgumentException("El ID del cargo no es válido.", nameof(cargoId));

            // 2. Obtener el Cargo (Viene DESCONECTADO por el MapToEntity del repo)
            var cargoEntity = await _readCargoRepository.GetByIdAsync(cargoId);

            if (cargoEntity == null)
                throw new KeyNotFoundException($"No se encontró el cargo con id {cargoId}");

            if (cargoEntity.EstudianteId != dto.EstudianteId)
                throw new InvalidOperationException("El estudiante del pago no coincide con el estudiante del cargo.");

            if (cargoEntity.SaldoPendiente <= 0)
                throw new InvalidOperationException("Este cargo ya está totalmente saldado.");

            // 3. Cálculos
            decimal montoAAplicar = Math.Min(dto.MontoRecibido, cargoEntity.SaldoPendiente);

            // 4. Actualizar la Entidad Cargo (En memoria del Dominio)
            cargoEntity.ActualizarPago(montoAAplicar);

            // 5. Crear la Entidad Pago
            var pagoEntity = new PagoEntity(
                dto.EstudianteId,
                DateTime.UtcNow,
                dto.MontoRecibido,
                montoAAplicar
            );

            // 6. Persistencia Fase 1: Guardar Pago
            // (Tu repositorio PagoRepository ya debe tener el truco del ID y el SaveChanges interno o
            // el UnitOfWork lo manejará, pero necesitamos el ID para el paso 7).
            await _createPagoRepository.Create(pagoEntity);

            // Si tu PagoRepository NO hace SaveChanges interno, descomenta esto:
            // await _unitOfWork.SaveAsync(); 

            if (!pagoEntity.Id.HasValue)
                throw new InvalidOperationException("Error al generar el ID del pago.");

            // 7. Crear la Transacción
            var transaccion = new TransaccionPagoEntity(
                pagoEntity.Id.Value,
                cargoEntity.Id.Value,
                montoAAplicar,
                DateTime.UtcNow
            );

            await _createTransaccionPagoRepository.Create(transaccion);

            // 8. Persistencia Fase 2: Actualizar Cargo y Guardar Transacción

            // IMPORTANTE: Como cargoEntity está desconectada, hay que avisar explícitamente 
            // al repositorio que debe actualizarse en la BD.
            await _updateCargoRepository.Update(cargoEntity);

            // Finalmente, confirmamos todos los cambios (Transacción y Update de Cargo)
            await _unitOfWork.SaveAsync();
        }


    }
}
