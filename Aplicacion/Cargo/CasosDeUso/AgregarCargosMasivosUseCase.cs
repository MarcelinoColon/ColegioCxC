using Aplicacion.Cargo.DTOs;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cargo.CasosDeUso
{
    public class AgregarCargosMasivosUseCase : ICreateUseCase<CargoInsertDto, CargoEntity>
    {
        private readonly ICreateRangeRepository<CargoEntity> _repository;
        private readonly IReadRepository<ConceptoEntity> _conceptoRepository;
        private readonly IRangeValidateRepository<EstudianteEntity> _validarEstudianteRepository;

        public AgregarCargosMasivosUseCase(ICreateRangeRepository<CargoEntity> repository, 
            IReadRepository<ConceptoEntity> conceptoRepository)
        {
            _repository = repository;
            _conceptoRepository = conceptoRepository;
        }

        public async Task AddAsync(CargoInsertDto dto)
        {
            if (dto == null) 
                throw new ArgumentNullException(nameof(dto));
            if(!dto.EstudiantesIds.Any())
                throw new ArgumentException("Debe selecionar almenos 1 estudiante", nameof(dto.EstudiantesIds));

            if (!(await _validarEstudianteRepository.Validate(dto.EstudiantesIds)))
                throw new ArgumentException("Uno o mas estudiantes no existen", nameof(dto.EstudiantesIds));


            var concepto = await _conceptoRepository.GetByIdAsync(dto.ConceptoId);

            if (concepto == null)
                throw new ArgumentException("Ingrese un concepto valido", nameof(dto.ConceptoId));

            var cargosEntities = new List<CargoEntity>();
            var fechaActual = DateTime.Now;

            foreach(var estudianteId in dto.EstudiantesIds.Distinct())
            {
                var cargoEntity = new CargoEntity(estudianteId, dto.ConceptoId, fechaActual,totalCargo: concepto.Monto,
                    totalPagado: 0,saldoPendiente: concepto.Monto, "Pendiente", null);

                cargosEntities.Add(cargoEntity);
            }

            await _repository.AddRange(cargosEntities);
        }
    }
}
