using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface ICreateRepository<TEntity>
    {
        Task Create(TEntity entity);
    }
}
