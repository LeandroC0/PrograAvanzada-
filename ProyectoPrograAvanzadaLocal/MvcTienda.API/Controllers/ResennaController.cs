using MvcTienda.Aplicacion.Common;
using MvcTienda.Aplicacion.Resennas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MvcTienda.API.Controllers
{
    [RoutePrefix("api/resennas")]
    public class ResennasController : ApiController
    {
        private readonly IResennaService _service;

        public ResennasController(IResennaService service)
        {
            _service = service;
        }

        // GET api/resennas
        [HttpGet]
        [Route("")]
        public IHttpActionResult Get()
        {
            try
            {
                var resennas = _service.GetAll();
                return Ok(resennas);
            }
            catch (Exception)
            {
                // Aquí conviene loguear
                return InternalServerError();
            }
        }

        // GET api/resennas/5
        [HttpGet]
        [Route("{id:int}", Name = "GetResennaById")]
        public IHttpActionResult Get(int id)
        {
            try
            {
                var resenna = _service.GetById(id);
                return Ok(resenna);
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

        // POST api/resennas
        [HttpPost]
        [Route("")]
        public IHttpActionResult Post([FromBody] ResennaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _service.Create(dto);

                // Devolver 201 Created con Location header
                return CreatedAtRoute(
                    "GetResennaById",
                    new { id = dto.ResennaId },   // Ojo: si el Id se genera en BD, deberías recuperarlo
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

        // PUT api/resennas/5
        [HttpPut]
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody] ResennaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Garantizar consistencia id en URL y body
            if (dto.ResennaId != 0 && dto.ResennaId != id)
                return BadRequest("Id del body no coincide con el de la URL.");

            dto.ResennaId = id;

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

        // DELETE api/resennas/5
        [HttpDelete]
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
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