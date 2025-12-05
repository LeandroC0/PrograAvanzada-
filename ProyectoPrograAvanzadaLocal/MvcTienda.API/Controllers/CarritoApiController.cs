using Microsoft.AspNet.Identity;
using MvcTienda.Aplicacion.Carrito;
using MvcTienda.Aplicacion.Ordenes;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MvcTienda.Web.Api
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [Authorize(Roles = "Asociado")]
    [RoutePrefix("api/carrito")]
    public class CarritoApiController : ApiController
    {
        private const string SESSION_KEY = "Carrito";

        private readonly IProductoService _productoService;
        private readonly IOrdenService _ordenService;

        public CarritoApiController(
            IProductoService productoService,
            IOrdenService ordenService)
        {
            _productoService = productoService;
            _ordenService = ordenService;
        }

        private CarritoDto GetCarrito()
        {
            var ctx = HttpContext.Current;
            var carrito = ctx.Session[SESSION_KEY] as CarritoDto;

            if (carrito == null)
            {
                carrito = new CarritoDto();
                ctx.Session[SESSION_KEY] = carrito;
            }

            return carrito;
        }

        private void SaveCarrito(CarritoDto carrito)
        {
            HttpContext.Current.Session[SESSION_KEY] = carrito;
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(GetCarrito());
        }

        [HttpPost]
        [Route("agregar")]
        public async Task<IHttpActionResult> Agregar(ItemCarritoDto model)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine(
                    "📌 API ENTRANDO AL MÉTODO AGREGAR");

                if (HttpContext.Current == null)
                    throw new Exception("HttpContext es NULL");

                if (HttpContext.Current.Session == null)
                    throw new Exception("Session es NULL en Web API");

                var carrito = GetCarrito();

                System.Diagnostics.Debug.WriteLine("📌 Sesión OK");

                var prod = await _productoService.GetByIdAsync(model.ProductoId);
                if (prod == null)
                    throw new Exception("ProductoService devolvió NULL");

                System.Diagnostics.Debug.WriteLine("📌 Producto cargado OK: " + prod.Nombre);

                carrito.Items.Add(new ItemCarritoDto
                {
                    ProductoId = prod.ProductoId,
                    NombreProducto = prod.Nombre,
                    PrecioUnitario = prod.Precio,
                    Cantidad = model.Cantidad
                });

                SaveCarrito(carrito);

                return Ok(carrito);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        [HttpPut]
        [Route("actualizar")]
        public IHttpActionResult Actualizar(ItemCarritoDto model)
        {
            var carrito = GetCarrito();
            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == model.ProductoId);

            if (item == null)
                return BadRequest("Producto no está en el carrito.");

            if (model.Cantidad <= 0)
                carrito.Items.Remove(item);
            else
                item.Cantidad = model.Cantidad;

            SaveCarrito(carrito);
            return Ok(carrito);
        }

        [HttpDelete]
        [Route("eliminar/{productoId:int}")]
        public IHttpActionResult Eliminar(int productoId)
        {
            var carrito = GetCarrito();
            var item = carrito.Items.FirstOrDefault(i => i.ProductoId == productoId);

            if (item != null)
            {
                carrito.Items.Remove(item);
                SaveCarrito(carrito);
            }

            return Ok(carrito);
        }

        [HttpDelete]
        [Route("vaciar")]
        public IHttpActionResult Vaciar()
        {
            SaveCarrito(new CarritoDto());
            return Ok(GetCarrito());
        }

        [HttpPost]
        [Route("confirmar")]
        public async Task<IHttpActionResult> Confirmar()
        {
            var carrito = GetCarrito();

            if (!carrito.Items.Any())
                return BadRequest("El carrito está vacío.");

            var userId = User.Identity.GetUserId<int>();

            var ordenId = await _ordenService.CrearOrdenDesdeCarritoAsync(
                userId,
                carrito.Items
            );

            Vaciar();
            return Ok(new { mensaje = "Orden registrada", ordenId });
        }
    }
}
