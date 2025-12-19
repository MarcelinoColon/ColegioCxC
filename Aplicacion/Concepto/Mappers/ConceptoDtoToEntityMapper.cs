using Aplicacion.Concepto.DTOs;
using Aplicacion.Interfaces.Mapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Concepto.Mappers
{
    public class ConceptoDtoToEntityMapper : IMapper<ConceptoDto, ConceptoEntity>
    {
        public ConceptoEntity Map(ConceptoDto conceptoDto)
        {
            if (conceptoDto == null)
                throw new ArgumentNullException(nameof(conceptoDto));

            var conceptoEntity = (conceptoDto.Id == null) ?
                new ConceptoEntity(conceptoDto.Nombre, conceptoDto.EsMora)
                :
                new ConceptoEntity((int)conceptoDto.Id, conceptoDto.Nombre, conceptoDto.EsMora);

            return conceptoEntity;
        }
    }
}
