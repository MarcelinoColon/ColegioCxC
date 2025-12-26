using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Paginacion;
using Aplicacion.Tutor.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Tutor.CasosDeUso
{
    public class ObtenerTutorUseCase : IReadUseCase<TutorDto, TutorEntity>
    {
        private readonly IReadRepository<TutorEntity> _repository;
        private readonly IMapper<TutorEntity, TutorDto> _mapper;
        public ObtenerTutorUseCase(IReadRepository<TutorEntity> repository, IMapper<TutorEntity, TutorDto> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TutorDto>> GetAll()
        {
            var tutoresEntity = await _repository.GetAllAsync();

            return tutoresEntity.Select(t => _mapper.Map(t));
        }

        public Task<PaginationDto<TutorDto>> GetAllPaginated(int pageSize, int currentPage)
        {
            throw new NotImplementedException();
        }

        public async Task<TutorDto> GetById(int id)
        {
            if(id<= 0)
                throw new ArgumentException("El ID debe ser un número positivo mayor que cero.", nameof(id));

            var tutorEntity = await _repository.GetByIdAsync(id);

            if(tutorEntity == null)
                throw new KeyNotFoundException($"No se encontró un tutor con el ID {id}.");

            return _mapper.Map(tutorEntity);

        }
    }
}
