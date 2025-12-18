using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Aplicacion.Tutor.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Tutor.Mappers
{
    public class TutorEntityATutorDtoMapper : IMapper<TutorEntity, TutorDto>
    {
        public TutorDto Map(TutorEntity tutorEntity)
        {
            if(tutorEntity == null)
                throw new ArgumentNullException(nameof(tutorEntity));

            var tutorDto = new TutorDto
            {
                Id = tutorEntity.Id,
                Nombre = tutorEntity.Nombre,
                Apellido = tutorEntity.Apellido,
                Cedula = tutorEntity.Cedula,
                Telefono = tutorEntity.Telefono,
                Estudiantes = tutorEntity.Estudiantes?.Select(e => new EstudianteDto
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Matricula = e.Matricula
                }).ToList() ?? new List<EstudianteDto>()
            };

            return tutorDto;
        }
    }
}
