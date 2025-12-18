using Aplicacion.Estudiante.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Tutor.DTOs
{
    public class TutorDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public List<EstudianteDto>? Estudiantes { get; set; }
    }
}
