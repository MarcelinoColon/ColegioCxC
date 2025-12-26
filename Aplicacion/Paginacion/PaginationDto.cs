using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Paginacion
{
    public class PaginationDto<T>
    {
        public List<T> Items { get; set; }
        public int TotalItems { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
    }
}
