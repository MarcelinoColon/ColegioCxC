using Aplicacion.Concepto.DTOs;
using Aplicacion.Interfaces.Mapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Concepto.Mappers
{
    public class ConceptoEntityToDtoMapper : IMapper<ConceptoEntity, ConceptoDto>
    {
        public ConceptoDto Map(ConceptoEntity conceptoEntity)
        {
            if (conceptoEntity == null)
                throw new ArgumentNullException(nameof(conceptoEntity));

            var conceptoDto = new ConceptoDto
            {
                Id = conceptoEntity.Id,
                Nombre = conceptoEntity.Nombre,
                EsMora = conceptoEntity.EsMora
            };

            return conceptoDto;
        }
    }
}
