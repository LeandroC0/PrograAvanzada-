using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Carrito;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MvcTienda.Web.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[Authorize(Roles = "Asociado")]
    [AllowAnonymous]
    [RoutePrefix("api/carrito")]
    public class CarritoApiController : ApiController
    {
        private const string SESSION_KEY = "Carrito";

        private readonly IProductoService _productoService;
        private readonly IOrdenService _ordenService;

        private static readonly Dictionary<string, CarritoDto> _carritos =
            new Dictionary<string, CarritoDto>();

        public CarritoApiController(
            IProductoService productoService,
            IOrdenService ordenService)
        {
            _productoService = productoService;
            _ordenService = ordenService;
        }

        private CarritoDto GetOrCreateCarrito(string carritoId)
        {
            if (string.IsNullOrEmpty(carritoId))
                throw new ArgumentException("carritoId no puede ser nulo o vacío.");
            if (!_carritos.ContainsKey(carritoId))
            {
                _carritos[carritoId] = new CarritoDto
                {
                    Items = new List<ItemCarritoDto>()
                };
            }
            return _carritos[carritoId];
        }

        [HttpPost]
        [Route("agregar")]
        public async Task<IHttpActionResult> Agregar(string carritoId, ItemCarritoDto model)
        {
            if (string.IsNullOrEmpty(carritoId))
                return BadRequest("Se requiere el carritoId.");

            var carrito = GetOrCreateCarrito(carritoId);

            var prod = await _productoService.GetByIdAsync(model.ProductoId);
            if (prod == null)
                return BadRequest("Producto no encontrado.");

            carrito.Items.Add(new ItemCarritoDto
            {
                ProductoId = prod.ProductoId,
                NombreProducto = prod.Nombre,
                PrecioUnitario = prod.Precio,
                Cantidad = model.Cantidad
            });

            return Ok(carrito);
        }


        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult Actualizar(string carritoId, ItemCarritoDto model)
        {
            var carrito = GetOrCreateCarrito(carritoId);

            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == model.ProductoId);
            if (item == null)
                return BadRequest("Producto no está en el carrito.");

            item.Cantidad = model.Cantidad;

            return Ok(carrito);
        }

        [HttpDelete]
        [Route("eliminar/{productoId:int}")]
        public IHttpActionResult Eliminar(string carritoId, int productoId)
        {
            var carrito = GetOrCreateCarrito(carritoId);
            carrito.Items.RemoveAll(i => i.ProductoId == productoId);
            return Ok(carrito);
        }

    //    [HttpPost]
    //    [Route("confirmar")]
    //    public async Task<IHttpActionResult> Confirmar()
    //    {
    //        var carrito = GetCarrito();

    //        if (!carrito.Items.Any())
    //            return BadRequest("El carrito está vacío.");

    //        var userId = User.Identity.GetUserId<int>();

    //        var ordenId = await _ordenService.CrearOrdenDesdeCarritoAsync(
    //            userId,
    //            carrito.Items
    //        );

    //        Vaciar();
    //        return Ok(new { mensaje = "Orden registrada", ordenId });
    //    }
    }
}
