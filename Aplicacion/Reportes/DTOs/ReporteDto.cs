using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Reportes.DTOs
{
    public class ReporteDto
    {
       public string titulo { get; set; }
         public List<Articulo> items { get; set; }



    }
    public class Articulo {
        public UInt32 id { get; set; }
        public string estudiante { get; set; }
        public float cargo { get; set; }
        public float abonado { get; set; }
        public float pendiente { get; set; }
        public string f_pagado { get; set; }
        public string f_vencimiento { get; set; }
        public string estado { get; set; }

    }
}
