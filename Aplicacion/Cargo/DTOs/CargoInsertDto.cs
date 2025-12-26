using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Cargo.DTOs
{
    public class CargoInsertDto
    {
        public List<int> EstudiantesIds { get; set; }
        public int ConceptoId { get; set; }
        public int? CargoId { get; set; }
    }
}
