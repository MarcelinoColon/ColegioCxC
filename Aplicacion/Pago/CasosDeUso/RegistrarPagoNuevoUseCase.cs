using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Pago.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Pago.CasosDeUso
{
    public class RegistrarPagoNuevoUseCase : ICreateUseCase<PagoDto, PagoEntity>
    {
        private readonly IReadRepository<CargoEntity> _readCargoRepository;
        private readonly ICreateRepository<PagoEntity> _createPagoRepository;
        private readonly ICreateRepository<TransaccionPagoEntity> _createTransaccionPagoRepository;
        private readonly IUnitOfWork _UnitOfWork;

        public RegistrarPagoNuevoUseCase(
            IReadRepository<CargoEntity> readCargoRepository,
            ICreateRepository<PagoEntity> createPagoRepository,
            ICreateRepository<TransaccionPagoEntity> createTransaccionPagoRepository,
            IUnitOfWork unitOfWork)
        {
            _readCargoRepository = readCargoRepository;
            _createPagoRepository = createPagoRepository;
            _createTransaccionPagoRepository = createTransaccionPagoRepository;
            _UnitOfWork = unitOfWork;
        }

        public async Task AddAsync(PagoDto dto, int cargoId)
        {
            if(dto == null)
                throw new ArgumentNullException(nameof(dto));

            var cargoEntity = await _readCargoRepository.GetByIdAsync(cargoId);

            if(cargoEntity == null)
                throw new ArgumentNullException(nameof(cargoId));

            var montoAAplicar = Math.Min(dto.MontoRecibido, cargoEntity.SaldoPendiente);

            var saldoParaBilletera = dto.MontoRecibido - montoAAplicar;

            var pagoEntity = new PagoEntity(dto.EstudianteId, DateTime.UtcNow, dto.MontoRecibido, montoAAplicar);
        }


    }
}
