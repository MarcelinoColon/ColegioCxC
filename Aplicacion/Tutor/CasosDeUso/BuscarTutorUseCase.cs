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
    public class BuscarTutorUseCase : ISearchUseCase<TutorDto, TutorEntity>
    {
        private readonly ISearchRepository<TutorEntity> _searchRepository;
        private readonly IMapper<TutorEntity, TutorDto> _mapper;
        public BuscarTutorUseCase(ISearchRepository<TutorEntity> searchRepository, IMapper<TutorEntity, TutorDto> mapper)
        {
            _searchRepository = searchRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TutorDto>> SearchAsync(string searchTerm)
        {
            var tutoresEntities = await _searchRepository.SearchAsync(searchTerm);

            return tutoresEntities.Select(tutorEntity => _mapper.Map(tutorEntity));
        }
    }
}
