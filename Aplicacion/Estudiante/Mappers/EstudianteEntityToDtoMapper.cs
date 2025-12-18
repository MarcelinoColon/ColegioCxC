using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Tutor.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Estudiante.Mappers
{
    public class EstudianteEntityToDtoMapper : IMapper<EstudianteEntity, EstudianteDto>
    {
        public EstudianteDto Map(EstudianteEntity input)
        {
            if(input == null) 
                throw new ArgumentNullException(nameof(input));

            var estudianteDto = new EstudianteDto
            {
                Id = input.Id,
                Nombre = input.Nombre,
                Apellido = input.Apellido,
                Matricula = input.Matricula,
                Tutores = input.Tutores?.Select(t => new TutorDto
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    Apellido = t.Apellido,
                    Cedula = t.Cedula,
                    Telefono = t.Telefono
                }).ToList() ?? new()
            };

            return estudianteDto;
        }
    }
}
