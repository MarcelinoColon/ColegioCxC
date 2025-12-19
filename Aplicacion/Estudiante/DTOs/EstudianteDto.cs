using Aplicacion.Tutor.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aplicacion.Estudiante.DTOs
{
    public class EstudianteDto
    {
        public int? Id { get;  set; }
        public string Nombre { get;  set; }
        public string Apellido { get;  set; }
        public string Matricula { get;  set; }
        public List<TutorDto>? Tutores { get; set; }
        public List<int>? TutoresIds { get; set; }
    }
}
