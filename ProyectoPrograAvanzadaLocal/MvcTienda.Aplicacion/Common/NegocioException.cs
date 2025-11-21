using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcTienda.Aplicacion.Common
{
    public class NegocioException : Exception
    {
        public NegocioException(String mensaje) : base (mensaje)
        {
        }
    }
}
