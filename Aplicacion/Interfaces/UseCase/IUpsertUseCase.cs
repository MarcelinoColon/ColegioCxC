using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.UseCase
{
    public interface IUpsertUseCase<TDTO, TEntity>
    {
        Task UpsertAsync(TDTO dto);
    }
}
