using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;

namespace MvcTienda.Aplicacion.Ordenes
{
    public class OrdenDto
    {
        public int ID_Orden { get; set; }
        public DateTime Fecha_Orden { get; set; }
        public decimal Total { get; set; }
        public string ID_Usuario { get; set; }
        public int ID_Estado { get; set; }

        
    }
}
