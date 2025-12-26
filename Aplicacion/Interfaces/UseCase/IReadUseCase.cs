using Aplicacion.Paginacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.UseCase
{
    public interface IReadUseCase<TDTO, TEntity>
    {
        Task<IEnumerable<TDTO>> GetAll();
        Task<TDTO> GetById(int id);
        Task<PaginationDto<TDTO>> GetAllPaginated(int pageSize, int currentPage);
    }
}
