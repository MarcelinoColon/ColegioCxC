using Aplicacion.Concepto.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Aplicacion.Paginacion;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Concepto.CasosDeUso
{
    public class ObtenerConceptoUseCase : IReadUseCase<ConceptoDto, ConceptoEntity>
    {
        private readonly IReadRepository<ConceptoEntity> _repository;
        private readonly IMapper<ConceptoEntity, ConceptoDto> _mapper;
        public ObtenerConceptoUseCase(IReadRepository<ConceptoEntity> repository, 
            IMapper<ConceptoEntity, ConceptoDto> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ConceptoDto>> GetAll()
        {
            var conceptos = await _repository.GetAllAsync();

            return conceptos.Select(c => _mapper.Map(c));
        }

        public Task<PaginationDto<ConceptoDto>> GetAllPaginated(int pageSize, int currentPage)
        {
            throw new NotImplementedException();
        }

        public async Task<ConceptoDto> GetById(int id)
        {
            var concepto = await _repository.GetByIdAsync(id);

            if(concepto == null)
                throw new ArgumentNullException(nameof(concepto));

            return _mapper.Map(concepto);
        }
    }
}
