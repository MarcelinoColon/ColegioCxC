using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Repository
{
    public interface IUnitOfWork
    {
        Task SaveAsync();
    }
}
