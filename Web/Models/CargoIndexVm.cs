using Aplicacion.Cargo.DTOs;
using Aplicacion.Paginacion;

namespace Web.Models
{
    public class CargoIndexVm
    {
        public PaginationDto<CargoDto> Pagination { get; set; }

        public List<int> SelectOptions { get; set; } = [5, 10, 15];
    }
}
