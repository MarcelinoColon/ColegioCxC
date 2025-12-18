using Aplicacion.Estudiante.DTOs;
using Aplicacion.Interfaces.Mapper;
using Dominio;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Estudiante.Mappers
{
    public class EstudianteDtoToEntityMapper : IMapper<EstudianteDto, EstudianteEntity>
    {
        public EstudianteEntity Map(EstudianteDto estudianteDto)
        {
            var estudianteEntity = (estudianteDto.Id == null) ? new EstudianteEntity(estudianteDto.Nombre, 
                estudianteDto.Apellido, estudianteDto.Matricula)
                :
                new EstudianteEntity((int)estudianteDto.Id, estudianteDto.Nombre,
                estudianteDto.Apellido, estudianteDto.Matricula);

            if(estudianteDto.Tutores != null && estudianteDto.Tutores.Count > 0)
            {
                foreach(var tutor in estudianteDto.Tutores)
                {
                    estudianteEntity.AgregarTutor(new TutorEntity((int)tutor.Id, tutor.Nombre, tutor.Apellido,
                        tutor.Cedula, tutor.Telefono));
                }
            }

            return estudianteEntity;
        }
    }
}