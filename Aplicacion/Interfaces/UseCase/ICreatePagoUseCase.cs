using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.UseCase
{
    public interface ICreatePagoUseCase<TDTO, TEntity>
    {
        Task AddAsync(TDTO dto, int cargoId);
    }
}
