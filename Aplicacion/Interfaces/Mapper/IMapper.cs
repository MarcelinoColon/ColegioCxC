using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicacion.Interfaces.Mapper
{
    public interface IMapper<TIn, TOut>
    {
        TOut Map(TIn input);
    }
}
