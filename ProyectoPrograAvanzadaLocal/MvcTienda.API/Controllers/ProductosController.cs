using MvcTienda.Aplicacion.Common;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MvcTienda.API.Controllers
{
    [RoutePrefix("api/productos")]
    public class ProductosController : ApiController
    {
        private readonly IProductoService _service;

        public ProductosController(IProductoService service)
        {
            _service = service;
        }

        // GET api/productos
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetAll()
        {
            var productos = _service.GetAll();
            return Ok(productos);
        }

        // GET api/productos/5
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult Get(int id)
        {
            var producto = _service.GetById(id);
            if (producto == null) return NotFound();

            return Ok(producto);
        }

        // POST api/productos (Crear)
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create([FromBody] ProductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _service.Create(dto);
            return Ok(new { mensaje = "Producto creado correctamente" });
        }

        // PUT api/productos/{id} (Editar)
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Update(int id, [FromBody] ProductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            dto.ProductoId = id;
            _service.Update(dto);

            return Ok(new { mensaje = "Producto actualizado correctamente" });
        }

        [HttpPut]
        [Route("{id:int}/estado")]
        public IHttpActionResult CambiarEstado(int id, [FromBody] ProductoDto dto)
        {
            try
            {
                var producto = _service.GetById(id);

                if (producto == null)
                    return NotFound();

                // Solo actualizar estado, sin modificar nada más
                _service.ChangeStatus(id, dto.EstadoId);

                return Ok(new { mensaje = "Estado actualizado correctamente" });
            }
            catch (NegocioException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
