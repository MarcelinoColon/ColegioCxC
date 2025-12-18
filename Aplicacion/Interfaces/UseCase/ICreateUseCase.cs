using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.UseCase
{
    public interface ICreateUseCase<TDTO, TEntity>
    {
        Task AddAsync(TDTO dto);
    }
}
