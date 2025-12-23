using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface IRangeValidateRepository<TEntity>
    {
        Task<bool> Validate(IEnumerable<int> ids);
    }
}
