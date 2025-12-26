using Aplicacion.Paginacion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface IReadRepository<TEntity>
    {
        public Task<TEntity> GetByIdAsync(int id);
        public Task<IEnumerable<TEntity>> GetAllAsync();
        public Task<(IEnumerable<TEntity> Items, int TotalRecords)> GetAllPaginatedAsync(int pageSize, int currentPage);
    }
}
