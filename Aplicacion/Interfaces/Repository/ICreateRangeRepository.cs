using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface ICreateRangeRepository<TEntity>
    {
        Task AddRange(IEnumerable<TEntity> entities);
    }
}
