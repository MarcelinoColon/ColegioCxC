using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Tutor
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Cedula { get; set; }
        public string? Telefono { get; set; }
        public List<Estudiante>? Estudiantes { get; set; }
    }
}
