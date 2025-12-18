using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface IUpdateRepository<TEntity>
    {
        Task Update(TEntity entity);
    }
}
