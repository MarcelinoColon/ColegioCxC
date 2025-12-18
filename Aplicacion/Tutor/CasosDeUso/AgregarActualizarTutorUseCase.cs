using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Tutor.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Tutor.CasosDeUso
{
    public class AgregarActualizarTutorUseCase : IUpsertUseCase<TutorDto, TutorEntity>
    {
        private ICreateRepository<TutorEntity> _createRepository;
        private IUpdateRepository<TutorEntity> _updateRepository;
        private IMapper<TutorDto, TutorEntity> _mapper;
        public AgregarActualizarTutorUseCase(ICreateRepository<TutorEntity> createRepository, 
            IMapper<TutorDto, TutorEntity> mapper, IUpdateRepository<TutorEntity> updateRepository)
        {
            _createRepository = createRepository;
            _updateRepository = updateRepository;
            _mapper = mapper;
        }
        public async Task UpsertAsync(TutorDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map(dto);

            if ( dto.Id.HasValue && dto.Id > 0)
            {
                await _updateRepository.Update(entity);
            }
            else
            {
                await _createRepository.Create(entity);
            }
        }

        public async Task AddAsync(TutorDto dto)
        {
            if(dto == null)
               throw new ArgumentNullException(nameof(dto));

            var entity = _mapper.Map(dto);

            await _createRepository.Create(entity);

        }

    }
}
