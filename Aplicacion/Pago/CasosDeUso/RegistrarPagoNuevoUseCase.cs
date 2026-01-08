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

        public Task AddAsync(PagoDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
