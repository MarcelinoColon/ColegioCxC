using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.UseCase
{
    public interface ISearchUseCase<TDto, TEntity>
    {
        Task<IEnumerable<TDto>> SearchAsync(string searchTerm);
    }
}
