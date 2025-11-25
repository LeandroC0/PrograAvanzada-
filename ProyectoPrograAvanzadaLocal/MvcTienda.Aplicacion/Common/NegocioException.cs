using System;

namespace MvcTienda.Aplicacion.Common
{
    public class NegocioException : Exception
    {
        public NegocioException(String mensaje) : base(mensaje)
        {
        }
    }
}
