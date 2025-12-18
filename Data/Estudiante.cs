using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public class Estudiante
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Matricula { get; set; }
        public List<Tutor> Tutores { get; set; }
    }
}
