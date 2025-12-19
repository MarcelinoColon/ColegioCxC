using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Tutor.DTOs;
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
        private readonly IReadRepository<TutorEntity> _obtenerTutorRepository;
        public AgregarActualizarEstudianteUseCase(ICreateRepository<EstudianteEntity> createRepository,
                                                  IUpdateRepository<EstudianteEntity> updateRepository,
                                                  IMapper<EstudianteDto, EstudianteEntity> mapper,
                                                  IReadRepository<TutorEntity> obtenertutorRepository)
        {
            _createRepository = createRepository;
            _updateRepository = updateRepository;
            _mapper = mapper;
            _obtenerTutorRepository = obtenertutorRepository;
        }

        public async Task UpsertAsync(EstudianteDto estudianteDto)
        {
            if (estudianteDto == null)
                throw new ArgumentNullException(nameof(estudianteDto));

            var entity = _mapper.Map(estudianteDto);

            if(estudianteDto.TutoresIds != null && estudianteDto.TutoresIds.Count() > 0)
            {
                foreach (var id in estudianteDto.TutoresIds)
                {
                    var tutorEntity = await _obtenerTutorRepository.GetByIdAsync(id);
                    if (tutorEntity != null)
                    {
                        entity.AgregarTutor(tutorEntity);
                    }
                }
            }


            if (estudianteDto.Id.HasValue && estudianteDto.Id.Value > 0)
            {
                await _updateRepository.Update(entity);
            }
            else
            {
                await _createRepository.Create(entity);
            }
        }
    }
}
