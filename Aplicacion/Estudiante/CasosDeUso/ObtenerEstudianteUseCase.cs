using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Paginacion;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Estudiante.CasosDeUso
{
    public class ObtenerEstudianteUseCase : IReadUseCase<EstudianteDto, EstudianteEntity>
    {
        private readonly IReadRepository<EstudianteEntity> _repository;
        private readonly IMapper<EstudianteEntity, EstudianteDto> _mapper;

        public ObtenerEstudianteUseCase(IReadRepository<EstudianteEntity> repository,
                                       IMapper<EstudianteEntity, EstudianteDto> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EstudianteDto>> GetAll()
        {
            var estudiantesEntities = await _repository.GetAllAsync();

            return estudiantesEntities.Select(e => _mapper.Map(e));
        }

        public Task<PaginationDto<EstudianteDto>> GetAllPaginated(int pageSize, int currentPage)
        {
            throw new NotImplementedException();
        }

        public async Task<EstudianteDto> GetById(int id)
        {
            if(id<= 0)
                throw new ArgumentException("El ID debe ser un número positivo mayor que cero.", nameof(id));

            var estudianteEntity =  await _repository.GetByIdAsync(id);

            return _mapper.Map(estudianteEntity);

        }
    }
}
