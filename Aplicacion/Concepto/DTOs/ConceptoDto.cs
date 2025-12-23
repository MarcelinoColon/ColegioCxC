using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Concepto.DTOs
{
    public class ConceptoDto
    {
        public int? Id { get; set; }
        public string Nombre { get; set; }
        public decimal Monto { get; set; }
        public bool EsMora { get; set; }
    }
}
