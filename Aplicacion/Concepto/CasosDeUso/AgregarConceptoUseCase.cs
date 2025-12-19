using Aplicacion.Concepto.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Interfaces.Repository;
using Aplicacion.Interfaces.UseCase;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Concepto.CasosDeUso
{
    public class AgregarConceptoUseCase : ICreateUseCase<ConceptoDto, ConceptoEntity>
    {
        private readonly ICreateRepository<ConceptoEntity> _createRepository;
        private readonly IMapper<ConceptoDto, ConceptoEntity> _mapper;
        public AgregarConceptoUseCase(ICreateRepository<ConceptoEntity> createRepository,
            IMapper<ConceptoDto, ConceptoEntity> mapper)
        {
            _createRepository = createRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(ConceptoDto dto)
        {
            if(dto == null) 
                throw new ArgumentNullException(nameof(dto));

            var conceptoEntity = _mapper.Map(dto);

            await _createRepository.Create(conceptoEntity);
        }
    }
}
