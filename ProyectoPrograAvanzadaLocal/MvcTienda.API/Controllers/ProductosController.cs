using MvcTienda.Aplicacion.Common;
using MvcTienda.Aplicacion.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public IHttpActionResult Get()
        {
            try
            {
                var productos = _service.GetAll();
                return Ok(productos);
            }
            catch (Exception)
            {
                // Aquí conviene loguear
                return InternalServerError();
            }
        }

        // GET api/productos/5
        [HttpGet]
        [Route("{id:int}", Name = "GetProductoById")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var producto = _service.GetById(id);
                return Ok(producto);
            }
            catch (NegocioException ex)
            {
                // Reglas de negocio violadas → 400
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // POST api/productos
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ProductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _service.Create(dto);

                // Devolver 201 Created con Location header
                return CreatedAtRoute(
                    "GetProductoById",
                    new { id = dto.ProductoId },   // Ojo: si el Id se genera en BD, deberías recuperarlo
                    dto
                );
            }
            catch (NegocioException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        // PUT api/productos/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ProductoDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Garantizar consistencia id en URL y body
            if (dto.ProductoId != 0 && dto.ProductoId != id)
                return BadRequest("Id del body no coincide con el de la URL.");

            dto.ProductoId = id;

            try
            {
                _service.Update(dto);
                return StatusCode(HttpStatusCode.NoContent); // 204
            }
            catch (NegocioException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
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

                producto.EstadoId = dto.EstadoId;

                _service.Update(producto);

                return Ok(new { mensaje = "Estado actualizado correctamente" });
            }
            catch (NegocioException ex)
            {
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [HttpGet]
        [Route("producto/{productoId:int}")]
        public IHttpActionResult GetImagenesPorProducto(int productoId)
        {
            var imagenes = _service.GetAll().Where(i => i.ProductoId == productoId && i.EstadoId == 1);

            return Ok(imagenes);
        }


    }
}
