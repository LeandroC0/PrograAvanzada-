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

        // DELETE api/productos/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult ChangeStatus(int id, int estadoId)
        {
            try
            {
                _service.ChangeStatus(id, estadoId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (NegocioException ex)
            {
                // Podrías usar 404 si el mensaje es "no existe", o 400 si es regla de negocio
                return Content(HttpStatusCode.BadRequest, ex.Message);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
