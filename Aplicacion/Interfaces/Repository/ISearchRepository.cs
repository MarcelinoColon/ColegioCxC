using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface ISearchRepository<TEntity>
    {
        Task<IEnumerable<TEntity>> SearchAsync(string searchTerm);
    }
}
