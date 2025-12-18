using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Estudiante.CasosDeUso
{
    public class AgregarActualizarEstudianteUseCase : IUpsertUseCase<EstudianteDto, EstudianteEntity>
    {
        private readonly ICreateRepository<EstudianteEntity> _createRepository;
        private readonly IUpdateRepository<EstudianteEntity> _updateRepository;
        private readonly IMapper<EstudianteDto, EstudianteEntity> _mapper;
        public AgregarActualizarEstudianteUseCase(ICreateRepository<EstudianteEntity> createRepository,
                                                  IUpdateRepository<EstudianteEntity> updateRepository,
                                                  IMapper<EstudianteDto, EstudianteEntity> mapper)
        {
            _createRepository = createRepository;
            _updateRepository = updateRepository;
            _mapper = mapper;
        }

        public Task UpsertAsync(EstudianteDto estudianteDto)
        {
            if (estudianteDto == null)
                throw new ArgumentNullException(nameof(estudianteDto));

            var entity = _mapper.Map(estudianteDto);

            if (estudianteDto.Id.HasValue && estudianteDto.Id.Value > 0)
            {
                return _updateRepository.Update(entity);
            }
            else
            {
                return _createRepository.Create(entity);
            }
        }
    }
}
