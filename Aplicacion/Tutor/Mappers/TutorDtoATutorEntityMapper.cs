using Aplicacion.Interfaces.Mapper;
using Aplicacion.Tutor.DTOs;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Tutor.Mappers
{
    public class TutorDtoATutorEntityMapper : IMapper<TutorDto, TutorEntity>
    {
        public TutorEntity Map(TutorDto tutorDto)
        {
            if(tutorDto == null)
                throw new ArgumentNullException(nameof(tutorDto));

            
            var tutorEntity = (tutorDto.Id == null)? 
                new TutorEntity(tutorDto.Nombre, tutorDto.Apellido, tutorDto.Cedula, tutorDto.Telefono)
                :
                new TutorEntity((int)tutorDto.Id, tutorDto.Nombre, tutorDto.Apellido, tutorDto.Cedula, tutorDto.Telefono);

            if (tutorDto.Estudiantes != null && tutorDto.Estudiantes?.Count > 0)
            {
                foreach (var estudiante in tutorDto.Estudiantes)
                {
                    var estudianteEntity = new EstudianteEntity((int)estudiante.Id, estudiante.Nombre, estudiante.Apellido, estudiante.Matricula);
                    tutorEntity.AgregarEstudiante(estudianteEntity);
                }
            }

            return tutorEntity;
        }
    }
}
