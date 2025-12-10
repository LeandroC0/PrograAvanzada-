using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcTienda.Aplicacion.Carrito
{
    public class CarritoDto
    {
        public List<ItemCarritoDto> Items { get; set; } = new List<ItemCarritoDto>();

        public decimal Total => Items.Sum(i => i.Subtotal);
    }
}
