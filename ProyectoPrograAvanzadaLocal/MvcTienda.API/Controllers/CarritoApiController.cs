using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Carrito;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MvcTienda.Web.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [AllowAnonymous]
    [RoutePrefix("api/carrito")]
    public class CarritoApiController : ApiController
    {
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

        // ============================================
        // GET /api/carrito?carritoId=...
        // ============================================
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get(string carritoId)
        {
            if (string.IsNullOrEmpty(carritoId))
                return BadRequest("Se requiere el carritoId.");

            var carrito = GetOrCreateCarrito(carritoId);

            return Ok(new
            {
                Items = carrito.Items.Select(i => new
                {
                    i.ProductoId,
                    i.NombreProducto,
                    i.PrecioUnitario,
                    i.Cantidad,
                    Subtotal = i.Cantidad * i.PrecioUnitario   
                }),
                Total = carrito.Items.Sum(i => i.Cantidad * i.PrecioUnitario)
            });
        }


        // ============================================
        // POST /api/carrito/agregar
        // ============================================
        [HttpPost]
        [Route("agregar")]
        public async Task<IHttpActionResult> Agregar(string carritoId, ItemCarritoDto model)
        {
            if (string.IsNullOrEmpty(carritoId))
                return BadRequest("Se requiere el carritoId.");

            if (model == null || model.ProductoId <= 0 || model.Cantidad <= 0)
                return BadRequest("Datos inválidos.");

            var carrito = GetOrCreateCarrito(carritoId);

            var prod = await _productoService.GetByIdAsync(model.ProductoId);
            if (prod == null)
                return BadRequest("Producto no encontrado.");

            var existente = carrito.Items.FirstOrDefault(i => i.ProductoId == prod.ProductoId);
            if (existente != null)
            {
                existente.Cantidad += model.Cantidad;
            }
            else
            {
                carrito.Items.Add(new ItemCarritoDto
                {
                    ProductoId = prod.ProductoId,
                    NombreProducto = prod.Nombre,
                    PrecioUnitario = prod.Precio,
                    Cantidad = model.Cantidad
                });
            }

            return Ok(carrito);
        }

        // ============================================
        // PUT /api/carrito/actualizar
        // ============================================
        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult Actualizar(string carritoId, ItemCarritoDto model)
        {
            var carrito = GetOrCreateCarrito(carritoId);

            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == model.ProductoId);
            if (item == null)
                return BadRequest("Producto no está en el carrito.");

            if (model.Cantidad <= 0)
                carrito.Items.Remove(item);
            else
                item.Cantidad = model.Cantidad;

            return Ok(carrito);
        }

        // ============================================
        // DELETE /api/carrito/eliminar/{id}
        // ============================================
        [HttpDelete]
        [Route("eliminar/{id:int}")]
        public IHttpActionResult Eliminar(string carritoId, int id)
        {
            var carrito = GetOrCreateCarrito(carritoId);

            carrito.Items.RemoveAll(i => i.ProductoId == id);

            return Ok(carrito);
        }

        // ============================================
        // POST /api/carrito/confirmar
        // ============================================
        [HttpPost]
        [Route("confirmar")]
        public async Task<IHttpActionResult> Confirmar(string carritoId, int usuarioId)
        {
            if (usuarioId <= 0)
                return BadRequest("Usuario no autenticado.");

            if (!_carritos.ContainsKey(carritoId) || !_carritos[carritoId].Items.Any())
                return BadRequest("El carrito está vacío.");

            var carrito = _carritos[carritoId];

            // Validar inventario
            foreach (var item in carrito.Items)
            {
                var prod = await _productoService.GetByIdAsync(item.ProductoId);
                if (prod == null)
                    return BadRequest($"El producto {item.ProductoId} ya no existe.");

                if (prod.Inventario < item.Cantidad)
                    return BadRequest($"No hay inventario suficiente para {prod.Nombre}.");
            }

            // Crear orden
            var ordenId = await _ordenService.CrearOrdenDesdeCarritoAsync(
                usuarioId,
                carrito.Items
            );

            _carritos.Remove(carritoId);

            return Ok(new
            {
                mensaje = "Orden registrada correctamente.",
                ordenId = ordenId
            });
        }
    }
}
